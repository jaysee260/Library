docker-compose down
docker-compose up -d

rm -rf Migrations/

dotnet ef migrations add InitialMigration
dotnet ef database update