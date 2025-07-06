# ChatMessAPI

API para gerenciamento de mensagens e salas de chat.

## Sumário

- [Visão Geral](#visão-geral)
- [Configuração](#configuração)
- [Endpoints Principais](#endpoints-principais)
- [CORS](#cors)
- [Health Checks](#health-checks)
- [Swagger/OpenAPI](#swaggeropenapi)
- [Logging](#logging)
- [Variáveis de Ambiente](#variáveis-de-ambiente)

---

## Visão Geral

Esta API foi desenvolvida em ASP.NET Core 8 e utiliza o padrão REST para gerenciamento de mensagens e salas de chat. Possui integração com health checks, logging estruturado (Serilog + Loki) e documentação automática via Swagger.

## Configuração

1. Certifique-se de ter o arquivo `.env` com as variáveis necessárias.
2. Configure as dependências e variáveis de ambiente conforme necessário.
3. Execute a aplicação com:
```bash
$ dotnet run
```


## Endpoints Principais

| Método | Rota                | Descrição                        |
|--------|---------------------|----------------------------------|
| GET    | `/health`           | Checagem simples de saúde        |
| GET    | `/health-details`   | Checagem detalhada de saúde      |
| *      | `/api/[controller]` | Endpoints dos controllers REST   |

> Os endpoints REST de mensagens e salas são definidos nos controllers correspondentes.

## CORS

A API permite requisições do frontend em `http://localhost:3000` com qualquer header e método, além de permitir credenciais.

## Health Checks

- **/health**: Retorna o status geral da aplicação.
- **/health-details**: Retorna status detalhado dos serviços monitorados.

## Swagger/OpenAPI

A documentação interativa está disponível na raiz da aplicação após o start:

- [http://localhost:5000/](http://localhost:5000/) (ajuste a porta conforme necessário)

## Logging

- Logs são enviados para o console, debug e, opcionalmente, para o Grafana Loki.
- As configurações de logging são controladas via variáveis de ambiente e seção `LoggerHelper`.

## Variáveis de Ambiente

A aplicação carrega variáveis do arquivo `.env` e do ambiente do sistema operacional.

---

## Observações

- Para detalhes dos endpoints de mensagens e salas, consulte os controllers e a documentação Swagger gerada automaticamente.
- Certifique-se de configurar corretamente as dependências de banco de dados e outros serviços externos.

