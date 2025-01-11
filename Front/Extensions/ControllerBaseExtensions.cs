using System.Net;
using Front.Models;
using Microsoft.AspNetCore.Mvc;

namespace Front.Extensions;

public static class ControllerBaseExtensions
{
    public static IActionResult ToActionResult(this ControllerBase thiz, BaseResponseJsModel response) =>
        string.IsNullOrEmpty(response.ErrorCode) ? thiz.Ok(response) : thiz.ToError(response.ErrorCode, response.ErrorDetail);
    
    public static IActionResult ToError(this ControllerBase thiz, string errorCode, string detail = null) =>
        errorCode switch
        {
            Core.Constants.ErrorCode.NotFound =>  thiz.Problem(HttpStatusCode.NotFound, detail),
            Core.Constants.ErrorCode.Conflict => thiz.Problem(HttpStatusCode.Conflict, detail),
            Core.Constants.ErrorCode.Forbidden => thiz.Problem(HttpStatusCode.Forbidden, detail),
            Core.Constants.ErrorCode.BadRequest => thiz.Problem(HttpStatusCode.BadRequest, detail),
            _ => thiz.Problem(HttpStatusCode.InternalServerError, errorCode),
        };
    
    
    private static IActionResult Problem(
        this ControllerBase thiz,
        HttpStatusCode statusCode,
        string detail = null
    ) =>
        thiz.Problem(statusCode: (int) statusCode, detail: detail);
}