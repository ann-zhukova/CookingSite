// frontend/webpack.config.js
const path = require('path')
const webpack = require('webpack')
module.exports = {
  devtool: 'source-map',//отвечает за подгрузку сурсов в девтулс
  // Where Webpack looks to load your JavaScript
  entry: {
    main: path.resolve(__dirname, 'src/index.js'),
  },
  mode: 'development',
  // Where Webpack spits out the results (the myapp static folder)
  output: {
    path: path.resolve(__dirname, '../wwwroot'),
    filename: '[name].js',
  },
  module: {
  rules: [
      {
        test: /\.css$/,
        use: ['style-loader', 'css-loader'],
      },
      {
        test: /\.(js|jsx)$/,
        exclude: /node_modules/,
        use: {
          loader: 'babel-loader',
          options: {
            presets: ['@babel/preset-env', '@babel/preset-react']
          }
        }
      },
      {
          test: /\.tsx?$/, // добавляем TypeScript
          exclude: /node_modules/,
          use: 'ts-loader',
      },
      {
          test: /\.(png|svg|jpg|jpeg|gif)$/i,
          type: 'asset/inline',
      },
      {
          test: /\.scss$/,
          use: [
              'style-loader',
              'css-loader',
              'sass-loader',
          ],
      },
      // Other loaders if any
    ],
  },
  plugins: [
    // Don't output new files if there is an error
    new webpack.NoEmitOnErrorsPlugin(),
  ],
  // Where find modules that can be imported (eg. React)
  resolve: {
    extensions: ['*', '.js', '.ts',  '.tsx', '.jsx', '.css', '.scss'],
    modules: [
        path.resolve(__dirname, 'src'),
        path.resolve(__dirname, 'node_modules'),
    ],
  },
}