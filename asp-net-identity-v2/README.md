
# ASP.NET Core Identity Management & Security


Ce cours est de udemy: [Voir le cours ici](https://www.udemy.com/course/aspnet-core-2-security-and-identity-management-with-c/?referralCode=1AA7034B27B8AAA89324)

[Le dépôt original GitHub est ici](https://github.com/aussiearef/asp-net-identity-v2?tab=readme-ov-file) 


[![ASP.NET Core 2 Security and Identity Management](https://img-c.udemycdn.com/course/750x422/1628048_aafb_14.jpg)](https://www.udemy.com/course/aspnet-core-2-security-and-identity-management-with-c/?referralCode=1AA7034B27B8AAA89324)


Le cours couvre également l'OWASP et le WAF, y compris une introduction aux systèmes WAF tiers couramment utilisés dans l'industrie.

## Getting Started

Please follow the below instructions and steps for each project:

1. Create an empty database in SQL Server. The database name used in the course is AspnetIdentityV2. If you name your database something else, please update the connection string in appSettings.json.
2. Update the appSettings.json with the correct details of the SQL Server database.
3. Update the appSettings.json with the correct details of the SMTP server when applicable.
4. Follow the instructions provided in the course and create the database objects using Entity Framework Migrations:
  ```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

