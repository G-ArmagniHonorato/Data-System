# Projeto Data System - Teste de Recrutamento

Este projeto foi desenvolvido para o processo seletivo da empresa Data System.

---

## Rodando o Backend (.NET 7.0)

1.  Vá até a pasta `DataSystem.API`.
2.  No arquivo `appsettings.json`, atualize a **connection string** para configurar seu banco de dados local.
3.  Execute as migrations com os seguintes comandos:
    ```bash
    dotnet ef migrations add init -s DataSystem.API -p DataSystem.Infrastructure
    dotnet ef database update -s DataSystem.API -p DataSystem.Infrastructure
    ```

---

## Rodando o Frontend (React)

1.  Navegue até a pasta do frontend:
    ```bash
    cd FrontEnd/task-front
    ```
2.  Instale as dependências:
    ```bash
    npm install
    ```
3.  Inicie a aplicação:
    ```bash
    npm start
    ```
