# ChatMessAPI

API para gerenciamento de mensagens e salas de chat.

## Sum�rio

- [Vis�o Geral](#vis�o-geral)
- [Configura��o](#configura��o)
- [Endpoints Principais](#endpoints-principais)
- [CORS](#cors)
- [Health Checks](#health-checks)
- [Swagger/OpenAPI](#swaggeropenapi)
- [Logging](#logging)
- [Vari�veis de Ambiente](#vari�veis-de-ambiente)

---

## Vis�o Geral

Esta API foi desenvolvida em ASP.NET Core 8 e utiliza o padr�o REST para gerenciamento de mensagens e salas de chat. Possui integra��o com health checks, logging estruturado (Serilog + Loki) e documenta��o autom�tica via Swagger.

## Configura��o

1. Certifique-se de ter o arquivo `.env` com as vari�veis necess�rias.
2. Configure as depend�ncias e vari�veis de ambiente conforme necess�rio.
3. Execute a aplica��o com:
```bash
$ dotnet run
```


## Endpoints Principais

| M�todo | Rota                | Descri��o                        |
|--------|---------------------|----------------------------------|
| GET    | `/health`           | Checagem simples de sa�de        |
| GET    | `/health-details`   | Checagem detalhada de sa�de      |
| *      | `/api/[controller]` | Endpoints dos controllers REST   |

> Os endpoints REST de mensagens e salas s�o definidos nos controllers correspondentes.

## CORS

A API permite requisi��es do frontend em `http://localhost:3000` com qualquer header e m�todo, al�m de permitir credenciais.

## Health Checks

- **/health**: Retorna o status geral da aplica��o.
- **/health-details**: Retorna status detalhado dos servi�os monitorados.

## Swagger/OpenAPI

A documenta��o interativa est� dispon�vel na raiz da aplica��o ap�s o start:

- [http://localhost:5000/](http://localhost:5000/) (ajuste a porta conforme necess�rio)

## Logging

- Logs s�o enviados para o console, debug e, opcionalmente, para o Grafana Loki.
- As configura��es de logging s�o controladas via vari�veis de ambiente e se��o `LoggerHelper`.

## Vari�veis de Ambiente

A aplica��o carrega vari�veis do arquivo `.env` e do ambiente do sistema operacional.

---

## Observa��es

- Para detalhes dos endpoints de mensagens e salas, consulte os controllers e a documenta��o Swagger gerada automaticamente.
- Certifique-se de configurar corretamente as depend�ncias de banco de dados e outros servi�os externos.

