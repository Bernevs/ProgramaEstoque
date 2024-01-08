# Sistema de Gerenciamento de Vendas
## Descrição
O Sistema de Gerenciamento de Vendas é uma aplicação desenvolvida em C# utilizando a arquitetura Model-View-Controller (MVC). Ele oferecendo funcionalidades para o gerenciamento de produtos, clientes, vendas e pagamentos.

## Funcionalidades
### Cadastrar Produto: 
Adiciona novos produtos ao estoque.
Atributos: nome, descrição, preço, quantidade em estoque, etc.
### Editar Produto:

Permite a alteração de informações de produtos existentes.
Atualiza campos como preço, quantidade em estoque, descrição, etc.
### Deletar Produto:
Remove um produto do estoque, garantindo integridade nos registros.
Clientes

### Cadastrar Cliente:
Registra novos clientes na base de dados.
Inclui informações como nome, endereço, contato, etc.

### Editar Cliente:
Permite a modificação de dados de clientes já cadastrados.
Atualiza informações como endereço, número de telefone, etc.

### Deletar Cliente:
Remove um cliente do sistema, assegurando que não afete registros de vendas ou pagamentos associados.
Vendas e Pagamentos

### Realizar Venda:
Registra uma venda, associando produtos e clientes.
Atualiza automaticamente o estoque.

### Realizar Pagamento:
Registra o pagamento associado a uma venda.
Inclui detalhes sobre o método de pagamento, valor pago, data, etc.

### Deletar Pedido:
Permite desfazer uma venda, restaurando o estoque e removendo registros associados.

### Deletar Pagamento:
Possibilita desfazer um registro de pagamento, revertendo as informações associadas.

### Registro de Atividades:
Mantém um histórico detalhado das atividades realizadas no sistema para fins de auditoria.
Usa o SQLite como banco de dados

## Interface Gráfica
HTML/CSS:
Interface amigável para facilitar a interação do vendedor com o sistema.
Permite cadastro, edição e exclusão de produtos, clientes, vendas e pagamentos.
