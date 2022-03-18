## Fundamentos de Desenvolvimento com C# - TP03

[Teste de Performance 03](https://lms.infnet.edu.br/moodle/mod/assign/view.php?id=276188)


### Status

[![Awesome Tomatoes TP03](https://github.com/msmagnanijr/csharp-mauricio-magnani/actions/workflows/build-csharp-tp03.yml/badge.svg)](https://github.com/msmagnanijr/csharp-mauricio-magnani/actions/workflows/build-csharp-tp03.yml)

### Sobre a Aplicação Awesome Tomatoes

Essa aplicação foi inspirada no Rotten Tomatoes que é um website americano, agregador de críticas de cinema e televisão. O objetivo do Awesome Tomatoes é implementar
o tema do projeto de bloco que nesse caso é um agregador de reviews ( filmes, séries, etc).

### Internacionalização

Atualmente a aplicação suporta dois idiomas: PT (Português) e EN (Inglês).

### Exemplo de Execução da Aplicação

![](images/app.gif)

### Funcionalidades Implementadas até o Momento

 - Adicionar um novo Filme
 - Listar todos os Filmes
 - Listar detalhes de um Filme

### Entidades Mapeadas até o Momento

 - Movies

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

### Padrões de Projeto Utilizados

 - Command
 - Repository
