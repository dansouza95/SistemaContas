Funcionalidade: Acessar o sistema

Cenário: Realizar login no sistema
Dado que estou na página "http://localhost:10941/Acesso/Login"
Então preencho o campo "Usuario" com o valor "dansouza.95"
E preencho o campo "Senha" com o valor "01020300"
Quando clico na opcao "Entrar"
Então devo ver o elemento "canvas"
E clico na opcao "Opcoes"
E clico na opcao "Sair"
Então devo ver o elemento "input"

Cenário: Errar o login
Dado que estou na página "http://localhost:10941/Acesso/Login"
Então preencho o campo "Usuario" com o valor "dansouza.95"
E preencho o campo "Senha" com o valor "senhaErrada"
Quando clico na opcao "Entrar"
Então devo ver a mensagem "Usuário ou senha inválidos!" no elemento "mensagem"

Cenário: Tentar cadastrar um cliente existente
Dado que estou na página "http://localhost:10941/Acesso/RegistroUsuario"
Então preencho o campo "txtNome" com o valor "Daniel"
E preencho o campo "txtEmail" com o valor "dansouza-95@uninove.edu.br"
E preencho o campo "txtUsuario" com o valor "dansouza.95"
E preencho o campo "txtSenha" com o valor "01020300"
Quando clico na opcao "btn_confirmar"
Então devo ver a mensagem "Email ou usuário já utilizados!" no elemento "mensagem"