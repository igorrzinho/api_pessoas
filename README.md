# API pessoas
api desenvolvida com C#.net8 usando o asp.net, com sqlserver e entityFramework com docker

## Tecnologias usadas:
 - C# asp.net8
 - SQLserver
 - Docker
 - Swagger UI

## Rodar

para rodar certifiquese de ter o docker intalado em sua maquina e use o seguinte comando na pasta da api
```` bash
docker compose --project-name pessoas up -d
````
- rota para obter todos as pessoas:
  - [http://localhost:4652/api/pessoas](http://localhost:4652/api/pessoas)

- rota do swagger para documentação:
    - [http://localhost:4652/swagger/index.html](http://localhost:4652/swagger/index.html)

## Conectar ao MSSQL para visualizar a base de dados
- server: localhost,1433
- user: sa
- senha: ola@mundo123