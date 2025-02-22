# Используем официальный образ .NET SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем файлы решения и восстанавливаем зависимости
COPY *.sln ./
COPY Core/Core.csproj ./Core/
COPY DataAccess/DataAccess.csproj ./DataAccess/
COPY Domain/Domain.csproj ./Domain/
COPY Front/Front.csproj ./Front/
RUN dotnet restore

# Копируем все файлы и собираем проекты
COPY . .
RUN dotnet publish -c release -o /app 

# Устанавливаем Node.js для работы с npm
FROM node:20 AS node-build
WORKDIR /app/Front/front_app
COPY Front/front_app/package*.json ./
RUN npm install

# Копируем фронтенд файлы
COPY Front/front_app ./
RUN npm run build &

# Используем официальный образ ASP.NET Core для runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Копируем собранные проекты
COPY --from=build /app ./

# Устанавливаем переменную окружения для строки подключения к базе данных
ENV ConnectionStrings__DefaultConnection="Host=172.17.0.1;Port=5432;Database=cook;Username=postgres;Password=sudo;"

# Запускаем приложение
ENTRYPOINT ["dotnet", "Front.dll"]