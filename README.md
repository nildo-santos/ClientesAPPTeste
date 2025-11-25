
# ClientesAPP — Web API com DDD + EF InMemory + ASP.NET Core 8

Este projeto consiste em uma API RESTful para gerenciamento de Clientes e seus Endereços, construída utilizando:

* ASP.NET Core 8

* DDD (Domain-Driven Design)

* Entity Framework Core (InMemory)

* AutoMapper

* xUnit para testes automatizados

* Swagger para documentação da API

A API permite criar, listar, buscar, atualizar e remover clientes, incluindo um relacionamento 1–1 com endereço.

### 📂 Arquitetura do Projeto (DDD)

O projeto está organizado em camadas seguindo DDD:

##  ClientesAPP

 ┣ 📁 Clientes.API            → Camada de Apresentação (Controllers, Program.cs)
 
 ┣ 📁 Clientes.Application     → Serviços, DTOs, Profiles do AutoMapper
 
 ┣ 📁 Clientes.Domain          → Entidades, Interfaces de Repositórios
 
 ┣ 📁 Clientes.Infra.Data      → EF Core InMemory, Repositórios
 
 ┗ 📁 ClientesAPP.Tests        → Testes automatizados com xUnit

Estrutura do Projeto

<img width="472" height="903" alt="Captura de tela 2025-11-25 173030" src="https://github.com/user-attachments/assets/4be94047-3aac-4d8d-944c-14bc3cef9f4f" />



## 🚀 Tecnologias Utilizadas

ASP.NET Core 8	Base da API

Entity Framework Core InMemory	Banco em memória

AutoMapper	Conversão entre DTOs e Entidades

xUnit + Moq	Testes unitários

Swagger	Documentação interativa

DDD	Separação por camadas e responsabilidades

📡 Endpoints da API

🔹 GET /clientes

Lista todos os clientes.

🔹 GET /clientes/{id}

Retorna os dados de um cliente específico.

🔹 POST /clientes

Cria um novo cliente.

📌 Validações:

Nome obrigatório

Email obrigatório

Email único

Endereço obrigatório

🔹 PUT /clientes/{id}

Atualiza um cliente existente.

🔹 DELETE /clientes/{id}

Remove um cliente.

🛢 Banco de Dados (InMemory)

Para facilitar testes e avaliação, o projeto utiliza o provider EF Core InMemory:

Não precisa instalar SQL Server

Banco é criado e descartado em memória


🔁 AutoMapper

O mapeamento das entidades e DTOs é realizado pelo profile:

MappingProfile.cs


Com suporte completo para Cliente e Endereco.

🔍 Validações Implementadas

✔ Nome obrigatório

✔ Email obrigatório

✔ Email válido

✔ Email único

✔ Todos os campos de endereço obrigatórios


🧪 Testes Automatizados (xUnit + Moq)

Foi criada uma classe completa de testes:


ClientesAPP.Tests/ClienteServiceTests.cs



Os testes cobrem:

✔ Sucesso

Criar cliente

Atualizar cliente

Consultar por ID

Consultar todos

Remover cliente

✔ Falhas

Email duplicado ao criar

Email duplicado ao atualizar

Cliente inexistente (obter, atualizar e deletar)

📸 Print dos Testes

INSERIR PRINT AQUI

📘 Swagger

O Swagger está habilitado automaticamente ao rodar o projeto.

Acessar:
http://localhost:5291/swagger/index.html


<img width="1706" height="921" alt="Captura de tela 2025-11-25 175615" src="https://github.com/user-attachments/assets/f80d0e8e-a96c-4a6f-b657-35561fa748f4" />


▶️ Como Executar o Projeto

1️⃣ Clonar o repositório:

git clone https://github.com/nildo-santos/ClientesAPPTeste

2️⃣ Entrar na pasta da API:

cd ClientesAPP

3️⃣ Executar:

dotnet run


A API estará disponível em:


https://localhost:{5291}


🛠 Como Executar os Testes


Na raiz da solução:

dotnet test

O que foi implementado


ASP.NET Core 8

Estrutura DDD

EF Core InMemory

AutoMapper

Swagger

xUnit Tests

CRUD Completo de Cliente

Validações completas

Email único

Relacionamento Cliente–Endereço

## 🧑‍💻 Autor

**Nildo Santos**\
