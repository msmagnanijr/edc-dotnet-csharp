## Projeto de Bloco: Desenvolvimento .NET - TP04 e TP05

[Teste de Performance 04](https://lms.infnet.edu.br/moodle/mod/assign/view.php?id=275913)

[Teste de Performance 05](https://lms.infnet.edu.br/moodle/mod/assign/view.php?id=275921)



### Diagrama de Camadas

```mermaid
%%{init:{"theme":"neutral"}}%%
flowchart TB

  Presentation --> Bussiness --> Data

  subgraph Presentation [Presentation Tier]
    A(Controller) <--> B[ View Model]
  end

  subgraph Bussiness [Business Logic Tier]
    C[Services] <--> D[Data Transfer Object] <--> E[Interace]
  end

  subgraph Data [Data Access Tier]
    G[Repository] <--> F[Mapping]
  end

```