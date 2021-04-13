Vue.use(VueResource);

var app = new Vue({
    el: '#app',
    data: {
        users: [],
        todos: [],
        displayTodo: false,
    },
    methods: {
        GetUsers: function () {
            this.$http.get('/api/UsersApi/GetUsers').then(response => {
                // get body data
                this.users = response.body;
            }, response => {
                // error callback
            });
        },

        GetTodosForUser: function (userId) {
            this.$http.get('/api/TodoApi/GetTodosForUser?userId=' + userId).then(response => {
                // get body data
                this.todos = response.body;
            }, response => {
                // error callback
            });
        },
    },
    created: function () {
        this.GetUsers();
    }
})

