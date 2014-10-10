Funcionalidade: Acessar o sistema

Cenário: Realizar login no sistema
Dado que estou na página "http://localhost:10941/Acesso/Login"
Então preencho o campo "Usuario" com o valor "dansouza.95"
E preencho o campo "Senha" com o valor "01020300"
Quando clico na opcao "Entrar"
Então devo ver o elemento "canvas"
E clico na opcao "Opcoes"
E clico na opcao "Sair"
Então devo ver o elemento "txtUsuario"