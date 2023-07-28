# API do GeekWorld (Rede social para todos que gostam de anime)

## Rotas da API

- Context Path: /api

Authentication
--------

POST: /login

    body:
        - email: string
        - password: string

POST: /sign-up

    body: 
        - fullName: string      * required
        - email: string         * required
        - nickname: string      * optional  
        - dateOfBirth: date     * required
        - cep: string;          * required
        - password: string      * required
        - imageProfile: string  * optional



Logged User
--------

GET: /me                                   [Buscar dados do perfil do usuário autenticado]

GET: /me/friendships                       [Ver solicitações de amizade]

GET: /me/posts                             [Buscar posts do usuário autenticado + posts dos amigos]

POST: /me/friendships                      [Fazer solicitação de amizade]

POST: /posts                               [Fazer um post na plataforma]

    body:
        - content: string
        - visibility: string

PATCH: /me/friendships                     [Responder solicitação de amizade]

PATCH: /me                                 [Atualizar perfil do usuário autenticado]

    body:
        - nickname: string
        - imageProfile: string



Users
--------

GET: /users

    query params:
        - search
        - page
        - limit

GET: /users/{user_id}



Posts
--------

POST: /posts/{post_id}/likes

POST: /posts/{post_id}/comments

    body:
        - content: string

GET: /posts{post_id}/likes

DELETE: /posts/{post_id}/likes

PATCH: /posts/{post_id}/visibility

    body:
        - visibility: string