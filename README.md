# DatloTest
## Teste vaga de Desenvolvedor(a) Back-end

## Contexto:
Imagine uma plataforma intuitiva e flexível que permite aos usuários importar um conjunto
de dados por meio de um arquivo CSV para realizar consultas e filtros, podendo vinculá-lo
ou não a outro conjunto de dados existente. Essa ferramenta é projetada para atender às
diversas necessidades de diferentes usuários, desde analistas de dados até gestores de
projetos.

## Informações adicionais:
- Leve em consideração que na plataforma já existe cadastrado um conjunto de dados
chamado Pokémons ( Pokémons.xlsx ).
- Para possibilitar futuras atualizações no conjunto de dados, é essencial que cada
conjunto contenha um identificador nela e nas colunas, permitindo que o sistema
identifique automaticamente cada coluna durante o processo de atualização.

## Casos de uso:
- O usuário deseja importar um arquivo ( Filmes da Marvel.xlsx ), que contém todos
os filmes da Marvel que ele assistiu no ano de 2023.
- O usuário deseja importar um arquivo ( Meus Pókemons.xlsx ), que contém todos
os Pokémon que ele capturou no ano de 2023. No entanto, esse arquivo possui
apenas o código de identificação de cada Pokémon, e ele gostaria de visualizar
todas as informações relacionadas a cada um. Para realizar isso, ele precisará
vincular o seu conjunto de dados ao conjunto de dados Pokémon, utilizando a
coluna de identificação como vínculo.

### Parte 1: Modelagem de Arquitetura
Modele a arquitetura do sistema, identificando as principais classes, interfaces e
relacionamentos. Considere a modularidade, a extensibilidade e a eficiência na
manipulação de grandes conjuntos de dados.

### Parte 2: Implementação Prática
Implemente uma versão simplificada do sistema de gerenciamento de bases que você
modelou na Parte 1, onde deve ser possível :
- Consultar e filtrar dados de qualquer conjunto de dados por meio de textos simples
ou números, dependendo do tipo de cada coluna.

### Observações:
- Compartilhe o código-fonte através de um repositório do Github.
- Priorize a qualidade do código, a modularidade e a legibilidade.
- Não hesite em fornecer explicações e justificativas para suas decisões de design e
implementação.