# Projeto Data System - Teste de Recrutamento

Este projeto foi desenvolvido para o processo seletivo da empresa Data System

Backend feito em .NET 7.0.

Para rodar a API:

1. Va ate a pasta `DataSystem.API` e atualize a connection string no arquivo `appsettings.json` para configurar com seu banco local

2. Execute as migrations com os comandos:
dotnet ef migrations add init -s DataSystem.API -p DataSystem.Infrastructure
dotnet ef database update -s DataSystem.API -p DataSystem.Infrastructure