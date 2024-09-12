using Projects;

var builder = DistributedApplication.CreateBuilder(args);

// DB 설정
var mysql = builder.AddMySql("mysql");
var mysqldb = mysql.AddDatabase("workshopdb");

// Backend 설정
builder.AddProject<BicycleSharingSystem_WebApi>("webApi")
       .WithReference(mysqldb);

// Aspire 시작!
builder.Build().Run();
