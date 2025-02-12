
# Sistema Taskool

### 🚨 Sobre
**Taskool** é um sistema simples que oferece login, cadastro, frases diárias, reprodução de músicas e personalização de cores, permitindo a seleção direta ou a inserção de um código Hexadecimal/RGB.

### 💡 Tecnologias Utilizadas
- C#
- Entity Framework 
- SQL Server


### 📌 Funcionalidades
- Login com e-mail ou nome de usuário, senha e credencial
- Cadastro de novos usuários
- Frases motivacionais na tela inicial
- Reprodução automática de músicas em sequência, com play e pausa
- Personalização de cores: use o Color Picker, código Hex ou RGB
- Registro de tentativas de login em um arquivo local
	- 📂 Um log será salvo automaticamente em C:\USER_LOGS\usuario.txt.

---
## 🚀 Como Usar
####  1️⃣ Clone o Repositório

#### 2️⃣  Configure o Banco de Dados
- Execute o script.sql fornecido.
- No **App.config** , edite o Data Source para o endereço correto do seu servidor SQL Server, garantindo que a aplicação funcione corretamente.
``` 
<connectionStrings>
	<add name="TaskoolEntities" ... data source=SEU_SERVIDOR; ... />
</connectionStrings> 
```

#### 3️⃣ Execute o Projeto no Visual Studio
- Abra o projeto no Visual Studio e execute-o para testar as funcionalidades.
