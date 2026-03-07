## Documentação da API (Controllers)

| Controller | Método | Rota (Endpoint) | Verbo | Descrição | Status |
| :--- | :--- | :--- | :---: | :--- | :---: |
| **Autenticacao** | `Login` | `/api/Autenticacao/login` | `POST` | Realiza a autenticação e retorna o Token JWT. | ✅ |
| **Classificacao** | `Listar` | `/api/classificacao` | `GET` | Lista todas as classificações indicativas. | ✅ |
| | `ObterPorId` | `/api/classificacao/{id}` | `GET` | Busca uma classificação específica por ID. | ✅ |
| **Genero** | `Listar` | `/api/Genero` | `GET` | Lista todos os gêneros de jogos. | ✅ |
| | `ObterPorId` | `/api/Genero/{id}` | `GET` | Busca um gênero específico por ID. | ✅ |
| | `Adicionar` | `/api/Genero` | `POST` | Cadastra um novo gênero (Requer Login). | ✅ |
| | `Atualizar` | `/api/Genero/{id}` | `PUT` | Atualiza um gênero existente (Requer Login). | ✅ |
| | `Remover` | `/api/Genero/{id}` | `DELETE` | Exclui um gênero do sistema (Requer Login). | ✅ |
| **LogProduto** | `Listar` | `/api/LogProduto` | `GET` | Lista todos os logs de alteração de produtos. | ✅ |
| | `ListarPorProduto` | `/api/LogProduto/produto/{id}` | `GET` | Filtra logs de um produto específico. | ✅ |
| **Plataforma** | `Listar` | `/api/Plataforma` | `GET` | Lista todas as plataformas disponíveis. | ✅ |
| | `ObterPorId` | `/api/Plataforma/{id}` | `GET` | Detalhes de uma plataforma específica. | ✅ |
| | `Adicionar` | `/api/Plataforma` | `POST` | Cadastra nova plataforma (Requer Login). | ✅ |
| | `Atualizar` | `/api/Plataforma/{id}` | `PUT` | Edita uma plataforma existente (Requer Login). | ✅ |
| | `Remover` | `/api/Plataforma/{id}` | `DELETE` | Remove uma plataforma (Requer Login). | ✅ |
| **Produto** | `Listar` | `/api/Produto` | `GET` | Lista o catálogo completo de produtos. | ✅ |
| | `ObterPorId` | `/api/Produto/{id}` | `GET` | Detalhes técnicos de um produto. | ✅ |
| | `Filtrar` | `/api/Produto/filtro` | `GET` | Busca avançada de produtos via query strings. | ✅ |
| | `Adicionar` | `/api/Produto` | `POST` | Cadastra produto via Multipart/Form-Data (Requer Login). | ✅ |
| | `Atualizar` | `/api/Produto/{id}` | `PUT` | Atualiza dados do produto (Requer Login). | ✅ |
| | `Remover` | `/api/Produto/{id}` | `DELETE` | Remove produto do catálogo (Requer Login). | ✅ |
| **ProdutoPromocao**| `Listar` | `/api/ProdutoPromocao` | `GET` | Lista vínculos entre produtos e promoções. | ✅ |
| | `ListarPorIds` | `/api/ProdutoPromocao/promocaoId/{pId}/produtoId/{prId}` | `GET` | Busca vínculo específico por IDs. | ✅ |
| | `Adicionar` | `/api/ProdutoPromocao` | `POST` | Vincula um produto a uma promoção. | ✅ |
| | `Atualizar` | `/api/ProdutoPromocao` | `PUT` | Altera valores de promoção (Requer Login). | ✅ |
| | `Remover` | `/api/ProdutoPromocao/promocaoId/{...}/produtuoId/{...}` | `DELETE` | Remove produto de uma promoção (Requer Login). | ✅ |
| **Promocao** | `Listar` | `/api/Promocao` | `GET` | Lista todas as campanhas promocionais. | ✅ |
| | `ObterPorId` | `/api/Promocao/{id}` | `GET` | Detalhes de uma campanha específica. | ✅ |
| | `Adicionar` | `/api/Promocao` | `POST` | Cria nova campanha (Requer Login). | ✅ |
| | `Atualizar` | `/api/Promocao/{id}` | `PUT` | Edita dados da campanha (Requer Login). | ✅ |
| | `Remover` | `/api/Promocao/{id}` | `DELETE` | Remove uma campanha (Requer Login). | ✅ |
| **Usuario** | `Listar` | `/api/Usuario` | `GET` | Lista todos os usuários cadastrados. | ✅ |
| | `ObterPorId` | `/api/Usuario/{id}` | `GET` | Busca perfil de usuário por ID. | ✅ |
| | `ObterPorEmail` | `/api/Usuario/email/{email}` | `GET` | Busca perfil de usuário por e-mail. | ✅ |
| | `Adicionar` | `/api/Usuario` | `POST` | Cadastra novo usuário (Requer Login). | ✅ |
| | `Atualizar` | `/api/Usuario/{id}` | `PUT` | Atualiza dados cadastrais (Requer Login). | ✅ |
| | `Remover` | `/api/Usuario/{id}` | `DELETE` | Remove conta de usuário (Requer Login). | ✅ |

---
> **Nota:** Métodos que possuem a tag `[Authorize]` exigem o envio do Token JWT no Header da requisição.