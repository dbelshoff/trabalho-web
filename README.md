# Sistema de Catalogo de Empresas (ASP.NET Core + Angular + MySQL)

Este projeto é uma aplicação web full-stack composta por um backend em **ASP.NET Core 8**, um frontend em **Angular 19**, e um banco de dados **MySQL**, para ser apresentado ao professor Vinicius Rosalem, na disciplina de Programação Avançada para Web, na Universidade Vila Velha.

---

## 📦 Tecnologias Utilizadas

### Backend (.NET Core)

- ASP.NET Core 8
- Entity Framework Core 8
- MySQL Connector
- Autenticação JWT
- Swagger (Swashbuckle)
- BCrypt.Net (criptografia de senhas)
- Newtonsoft.Json

### Frontend (Angular)

- Angular 19
- Angular Material
- RxJS
- TypeScript
- Karma + Jasmine (para testes)

### Banco de Dados

- MySQL

---

## 🚀 Como Executar o Projeto

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [Node.js e npm](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli) (`npm install -g @angular/cli`)
- [MySQL Server](https://www.mysql.com/)

---

### 🔧 Backend (.NET Core)

1. Acesse a pasta do backend:

```bash
cd backend
```

2. Configure a string de conexão no arquivo `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;database=nome_do_banco;user=root;password=senha"
}
```

3. Execute as migrações do Entity Framework:

```bash
dotnet ef database update
```

4. Inicie a aplicação:

```bash
dotnet run
```

> O backend estará disponível em: `https://localhost:5174`

---

### 💻 Frontend (Angular)

1. Acesse a pasta do frontend:

```bash
cd frontend
```

2. Instale as dependências:

```bash
npm install
```

3. Inicie o servidor de desenvolvimento:

```bash
ng serve
```

> O frontend estará disponível em: `http://localhost:4200`

---

## 📚 Funcionalidades

- Autenticação com token JWT
- Integração total com banco de dados MySQL
- API RESTful documentada via Swagger
- Interface moderna com Angular Material
- Cadastro, autenticação e listagem de dados
- Estrutura modular e escalável

---

## 🔒 Segurança

- Senhas criptografadas com **BCrypt**
- Autenticação com **JWT**
- Validação de usuários e rotas protegidas
- Configuração para uso de HTTPS

---

## 🛠 Scripts Úteis

### Backend

```bash
dotnet build                            # Compila o projeto
dotnet run                              # Executa o servidor
dotnet ef migrations add NomeDaMigracao # Cria nova migração
dotnet ef database update               # Aplica as migrações
```

### Frontend

```bash
npm start        # Inicia o servidor de desenvolvimento
ng build         # Gera a versão de produção
npm test         # Executa os testes
```

---

## 🧑‍💻 Desenvolvedor

- **Nome:** Diogo Belshoff
- **GitHub:** https://github.com/seu-usuario/dbelshoff
- **E-mail:** diogobelshoff@gmail.com

---

## 📄 Licença

Este projeto está licenciado sob a [MIT License](LICENSE).
