# Video Platform

The goal of the project is to replicate a Video Platform like Youtube.
Users can create accounts, can upload videos, can see other's videos 
and can leave feedback and comments.

## Getting Started

### Executing program

To run the project you must setup your sql conection in MyDbContext.cs file:
```
dotnet ef database update --proj "your_project_path"
```

Then you need to select as startup projects both BlazorClient and WebAPI projects.