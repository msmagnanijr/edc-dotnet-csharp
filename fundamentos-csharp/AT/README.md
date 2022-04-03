## Fundamentos de Desenvolvimento com C# - Assessment (WIP)

[Assessment](https://lms.infnet.edu.br/moodle/mod/assign/view.php?id=276211)


### Status


### Sobre a Aplicação Awesome Tomatoes

Essa aplicação foi inspirada no Rotten Tomatoes que é um website americano, agregador de críticas de cinema e televisão. O objetivo do Awesome Tomatoes é implementar
o tema do projeto de bloco que nesse caso é um agregador de reviews ( filmes, séries, etc).

### Internacionalização

A aplicação suporta dois idiomas: PT (Português) e EN (Inglês).

### Exemplo de Execução da Aplicação

![](images/app.gif)


A aplicação suporta 4 tipos de "Banco de Dados": Arquivos, List, LinkedList e Banco de Dados (SQL Server), bastando o usuário escolher ao iniciar a aplicação.

### Funcionalidades Implementadas até o Momento

 - Adicionar um novo Filme
 - Listar todos os Filmes
 - Listar detalhes de um Filme
 - Atualizar Informações de um Filme
 - Remover um Filme

### Entidades Mapeadas até o Momento


```mermaid
 classDiagram
 class Movies
  Movies : +Guid id
  Movies : +String name
  Movies : +String filmStudio
  Movies : +DateTime releaseDate
  Movies : +Double boxOffice
```

### Teste Unitário

NUnit está sendo utilizado para execução de testes unitários.

![](images/nunit.png)

### Dependências

 - Colorful.Console (1.2.15)
 - NUnit (3.3.12)
 - Microsoft.EntityFrameworkCore.SqlServer (6.0.2)
 - Microsoft.EntityFrameworkCore.Tools (6.0.2)

### Padrões de Projeto Utilizados

 - Command
 - Repository
 - Factory
