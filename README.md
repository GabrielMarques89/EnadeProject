# EnadeProject

Projeto de Desenvolvimento de um Framework base para API.

Esse projeto foi desenvolvido para desenvolver conhecimentos de estruturação de uma arquitetura consolidada, focada em produtividade e generalização.
O objetivo desse projeto é desenvolver uma plataforma fácilmente adaptável, que permita que CRUD's simples sejam desenvolvidos com agilidade, mantendo uma alto nível de segurança, eficiência e confiabilidade.

Um dos primeiros problemas enfrentados foi a dificuldade em mapear as provas ENADE.
As provas disponíveis no site oficial do INEP (órgão responsável pela aplicação do exame) sofreram um processo de compressão.
Essa compressão retira o mapeamento de caracteres. Isso resulta num texto "incopiável".
Portanto, foi necessário o uso de uma ferramenta de OCR (Optical Character Recognition - Reconhecimento ótico de caracteres).
A ferramenta utilizada para tal tarefa foi a api online da empresa ABBY (disponível em https://ocrsdk.com/for-students/)

### Pré-requisitos

* Visual Studio 2017 (C# 7.0)

* IIS (.net framework 4.6.1)

* Servidor My Sql

### Instalação

Após o download do projeto é necessário implantar o servidor de aplicação e configurar as strings de conexão.
O projeto está configurado para gerar as Queries DDL dos modelos mapeados.

### Built With

* Visual Studio
* ABBYY cloud OCR - Disponível em ocrsdk.com
* Asp.Net Boilerplate
* NHibernate

Muitos outros ótimos frameworks.

### Melhorias em planejamento

* Decisões sobre implementação automática dos endpoints pelas API's
* Sistema de login usando oAuth
* Implementação de OData

### Authors
*Gabriel Marques

### License

*Esse projeto é licenciado sobre uma licença MIT
