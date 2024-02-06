# Projeto de Cadastro de Aluno (CRUD)

## Descrição do Projeto

Este projeto visa desenvolver um sistema web para o cadastro de alunos, conforme os artefatos fornecidos, incluindo diagramas, protótipos de telas e requisitos do sistema.

## Ferramentas Utilizadas

### 1. IDE: Microsoft Visual Studio Community 2022
   - [Download do Visual Studio Community 2022](https://visualstudio.microsoft.com/pt-br/vs/community/)

### 2. Ferramenta para Administração de Banco de Dados: DBeaver Community
   - [Download do DBeaver Community](https://dbeaver.io/download/)

## Tecnologias Utilizadas

- ASP.NET MVC (linguagem C#)
- SGBD (FIREBIRD) - Sem utilização de framework ORM
   - [Download do Firebird](https://firebirdsql.org/en/firebird-4-0)
- HTML
- CSS (Bootstrap)
- JS vanilla

### Recursos da Linguagem Utilizados

- Generics
- LINQ
- Conversão de Tipos
- Downcast
- Upcast

## Requisitos do Projeto

- **Tela Inicial:** Exibir todos os alunos cadastrados (Consulte a imagem: AlunosCadastrados.png)
- **Tela de Cadastro/Edição:** Única tela para cadastro e edição de aluno. Título alterado para "Edição de Aluno" quando aplicável
- **Exclusão com Confirmação:** Confirmar a exclusão do aluno ao clicar no botão correspondente
- **CPF Opcional:** Preenchimento não obrigatório, mas válido quando informado
- **Campo NOME:** Obrigatório, alinhado à esquerda, com 3 a 100 caracteres
- **Campo SEXO:** Opções - Masculino e Feminino, somente leitura
- **Pesquisa de Aluno:** Por MATRÍCULA ou NOME
