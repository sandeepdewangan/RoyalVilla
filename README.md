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