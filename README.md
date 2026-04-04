# ASP.NET Core Web API

Application Programming Interface: Responsible for transfering the data.

## How HTTP Works

**Client Sends a Request**

When you type a URL or your app calls an API, your client sends an HTTP request to a server.

Request Example:

```
GET /api/users HTTP/1.1
Host: example.com
Authorization: Bearer token123
```
Key parts:

Method → What you want to do

* GET (read data)
* POST (create data)
* PUT/PATCH (update)
* DELETE (remove)

URL/Path → Where to send the request

Headers → Extra info (auth, content type, etc.)
* Content Type: Binary, JSON, XML etc.
* Content Length: Size of content.
* Authorization: who is requesting data
* Accept: What are request type.

Body → Data

**Server Sends Response**

Server returns an HTTP response.

Example response:
```
HTTP/1.1 200 OK
Content-Type: application/json

{
  "users": [
    { "id": 1, "name": "Sandeep" }
  ]
}
```
Key parts:

Status Code
* [200-299] → Success
* 200 → Success
* 201 → Created
* 204 → No Content
* [300-399] → Redirection
* [400-499] → Client Errors
* 400 → Bad request
* 401 → Unauthorized
* 404 → Not found
* [500-599] → Server Error
* 500 → Internal Server Error

Headers
* Content Type, Length, Expires...

Body → actual data (JSON, HTML, etc.)

## Create Project

1. Create a project named: Asp.net Core Web API
2. Project Name: RoyalVillaAPI
3. Solution Name: RoyalVilla
4. Make sure "Place solution and project in same directory" checkbox is unchecked. 
5. Authentication Type: None
6. ☑️ Unable OpenAPI support
7. ☑️ Use controllers

## Scalar
In ASP.NET Core, Scalar is a modern, open-source API documentation tool used as an interactive alternative to Swagger UI. It renders OpenAPI/Swagger specifications into a clean, developer-friendly interface with built-in testing capabilities and code snippet generation for over 25 languages.

STEP 01: Installation:

Using NuGet package manager install package: `Scalar.AspNetCore`

STEP 02: `Program.cs`

```cs
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    // For opening scalar API
    app.MapScalarApiReference();
}
```
STEP 03: By default API projects opened in Command promt. To force to open in Browser do following settings.

* Go to properties of project
* Under debug, goto open debug launch profiles UI and check launch browser.

Now scalar can be accessed via, https://localhost:7138/scalar

To navigate automatically when project is started, go to debug launch profiles and update the Url field to `scalar`

## Database Connection

**Install Packages**

`Npgsql.EntityFrameworkCore.PostgreSQL` 
`Microsoft.EntityFrameworkCore.Tools`

`appsettings.json`

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=RoyalVilla;Username=postgres;Password=sandeep"
}
```

**Migration**

```
add-migration Intial
update-database
```

## DTO Class

DTO (Data Transfer Object) is necessary to control what data goes in and out of your API.
* Hide sensitive fields (Password, Tokens, etc.)
* Prevent over-posting attacks
* What to send or what not to

## Automapper
If suppose i have VillaCreateDTO model inputs, and i want to map to Db binded Villa Model i can achive without automapper like below.

```cs
Villa villa = new Villa()
{
    Name = villaDto.Name,
    Details = villaDto.Details,
    ImageUrl = villaDto.ImageUrl,
    Occupancy = villaDto.Occupancy,
    Sqft = villaDto.Sqft,
    Rate = villaDto.Rate,
    CreatedDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
};
```
But with automapper i can do it easily,

Step 01: Install package automapper.

Step 02: program.cs

````cs
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// add auto mapper
builder.Services.AddAutoMapper(o =>
{
   o.CreateMap<Villa, VillaCreateDTO>().ReverseMap();
});
```

Step 03: Inject using DI

```cs
private readonly IMapper _mapper;
public VillaController(IMapper mapper)
{
    _mapper = mapper;
}
```

Step 04: Usage

```cs
 Villa villa = _mapper.Map<Villa>(villaDto);
 ```
