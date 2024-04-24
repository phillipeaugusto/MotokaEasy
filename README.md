# MotokaEasy

- Aplicação tem como objetivo gerenciar o aluguel de motos e entregadores.


## Pré-requisitos
- Docker instalado. Caso ainda não tenha, você pode baixar e instalar a partir do [site oficial do Docker](https://www.docker.com/).

## Instalação da Infraestrutura

1. Dentro do repositório, navegue até a pasta `infra`.

2. Dentro da pasta `infra`, você encontrará o arquivo `docker-compose.yml`.

3. Abra um terminal na pasta `infra` e execute o seguinte comando para iniciar os containers da infraestrutura:

    ```bash
    docker-compose up -d
    ```

    Isso irá instalar toda a infraestrutura necessária para executar a aplicação.

## Configuração do LocalStack (CloudLocal)

Após a instalação dos containers da infraestrutura, é necessário executar alguns comandos para configurar o LocalStack.

1. Abra um terminal e execute os seguintes comandos:

    ```bash
    aws s3 mb s3://motokaeasy-cnh-entregador --endpoint http://localhost:4566
    aws s3api put-bucket-acl --bucket motokaeasy-cnh-entregador --acl public-read-write --endpoint http://localhost:4566
    ```

    Isso criará um bucket S3 local e configurará as permissões necessárias.

## Execução das Migrações do Entity Framework (ORM)

Após configurar o ambiente, execute as migrações do Entity Framework para configurar o banco de dados.

1. Navegue até a pasta `MotokaEasy.Infra` e execute o seguinte comando para adicionar uma nova migração:

    ```bash
    dotnet ef --startup-project ../MotokaEasy.API/ migrations add 01 -c DataContext
    ```

2. Navegue até a pasta `MotokaEasy.API` e execute o seguinte comando para aplicar as migrações ao banco de dados:

    ```bash
    dotnet ef database update 01 -c DataContext
    ```

    Isso irá criar e configurar o banco de dados necessário para a aplicação.

Com isso, a aplicação está pronta para ser iniciada. 😊

---

## Informações Basicas
  Depois da configuração feita basta acessar o [swagger da aplicação](https://localhost:7212/swagger/index.html).
  
  - Para obter o token existe um endpoint v1/login com ele é possivel obter os tokens para poder acessar os endpoints. 

Segue os dados de acesso:
  - UsuarioAdministradorUser: Email = useradm@motokaeasy.com, Senha = 112233
  - UsuarioAdministrador: Email = adm@motokaeasy.com, Senha = 112233
  - UsuarioNormal: Email = user@motokaeasy.com, Senha = 112233

## Stack Utilizadas

- Donet
- C#
- RabbitMq
- Polly
- Postgres
- Memcached
- EntityFrameWork
- Docker
- XUnit
- Moq
