# AwesomeDevEvents

Referência
https://www.youtube.com/playlist?list=PLI2XdbZhEq4n9A46xhfYPMdViA3H_v-mb

Criação de CRUD API em .NetCore simples, com Entity, CodeFirst 



Executar os comandos do EF

//cria o Migrations co, base no modelo passado de nome "PrimeiraMigracao" na pasta "Persistence/Migrations"
dotnet ef migrations add PrimeiraMigracao -o Persistence/Migrations

//Remove o Migration, não fazer isso se já fez o update na base
dotnet ef migrations remove

//Atualizar Banco de dados com o Migration
dotnet ef database update