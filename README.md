# CRUD_Filmes_API

## Web API ASP.NET 5.0
#### Ações possíveis:
Listar todos os filmes;

Listar filme por Id;

Criar filme;

Atualizar filme;

Deletar um ou varios filmes.

Também é possível criar, listar e deletar Gêneros de filme e Locações.

#### A criação de gênero é necessária tendo em vista que filme possui uma FK desta identidade.

Para rodar o projeto localmente deve se ter uma instancia SQL Server e alterar o arquivo “appsettings.json” com os dados da sua instancia.
#### O database da aplicação será criado pelo migrations do Entity Framework, não havendo necessidade de scripts.

### A API está publicada no Azure, é possível fazer as requisições nas seguintes URLs:
Filmes: https://apicrudfilme.azure-api.net/v1/api/filmes

Gêneros: https://apicrudfilme.azure-api.net/v1/api/generos

Locações: https://apicrudfilme.azure-api.net/v1/api/locacaos

A documentação da API gerada pelo Swagger pode ser vista no seguinte caminho: 

https://app.swaggerhub.com/apis/Cassiano-Kretzmann/crud-filmes_api/v1


