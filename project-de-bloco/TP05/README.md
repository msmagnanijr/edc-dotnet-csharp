## Projeto de Bloco: Desenvolvimento .NET - TP04 e TP05

[Teste de Performance 04](https://lms.infnet.edu.br/moodle/mod/assign/view.php?id=275913)

[Teste de Performance 05](https://lms.infnet.edu.br/moodle/mod/assign/view.php?id=275921)



### Diagrama de Camadas - Awesome Tomatoes

```mermaid
%%{init:{"theme":"neutral"}}%%
flowchart TB

  Presentation --> Bussiness --> Data

  subgraph Presentation [Presentation Tier]
    A(Razor Pages) <--> B[ ASP .NET MVC Controllers]
  end

  subgraph Bussiness [Business Logic Tier]
    C[ASP .NET MVC Services] <--> D[ASP .NET MVC Data Transfer Object] <--> E[ASP .NET MVC Interace]
  end

  subgraph Data [Data Access Tier]
    G[Entity Framework] <--> F[ MS SQL Server]
  end

```

### Diagrama de Componentes - Awesome Tomatoes

```mermaid
%%{init:{"theme":"neutral"}}%%
flowchart TB

  Presentation --> Bussiness --> Data

  subgraph Presentation [Presentation Tier]
    A(Details.cshtml Index.cshtml ) <--> B[ MoviesController.cs]
  end

  subgraph Bussiness [Business Logic Tier]
    C[MovieFactory.cs] <--> D[MovieRepository.cs] <--> E[IMovieRepository.cs]
  end

  subgraph Data [Data Access Tier]
    G[Movie.cs] <--> F[EFContext.cs]
  end

```