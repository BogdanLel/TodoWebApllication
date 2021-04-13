Vue.use(VeeValidate, {
    classes: true,
    events: 'change',
    classNames: {
        valid: 'is-valid',
        invalid: 'is-invalid'
    }
});

Vue.use(VueResource);

var app = new Vue({
    el: '#app',
    data: {
        title: 'My first work app',
        newTodo: { Name: '', Status: false, Priority: 0, CurrentDateTime: '' },
        todos: [],
        priorities: [],
        users: [],
        doneTodos: { Id: 1, Name: '', Status: false, Priority: 0 },
        toBeDoneTodos: { Id: 1, Name: '', Status: false, Priority: 0 },
        selectedItem: {},
        todoCopy: {},
        itemS: { Status: false },
        filter: { CurrentPage: 1, PageSize: 6, FilterText: '', OrderBy: '' },
        filterDone: { CurrentPage: 1, PageSize: 6 },
        filterToBeDone: { CurrentPage: 1, PageSize: 6 },
        nrOfPages: 0,
        nrOfPagesDone: 0,
        nrOfPagesToBeDone: 0,
        searchText: '',
        selectedPriority: {},
        selectedUser: {}
    },
    methods: {
        ChangeStatus: function (todo) {
            if (todo.Status == true) {
                todo.Status = false;
            }
            else {
                todo.Status = true;
            }
        },

        CheckStatus: function (itemS) {
            if (itemS.Status) {
                this.newTodo.Status = true;
            }
        },

        GetTodos: function (page, order) {
            if (!page) {
                page = 1;
            }
            if (!order) {
                this.filter.OrderBy = '';
            }
            else {
                this.filter.OrderBy = order;
            }

            this.filter.CurrentPage = page;
            this.filter.FilterText = this.searchText;
            this.$http.post('/api/TodoApi/GetTodos', this.filter).then(response => {
                // get body data
                this.todos = response.body.Items;
                this.nrOfPages = response.body.NumberOfPages;
            }, response => {
                // error callback
            });
        },

        AddTodo: function () {
            const today = new Date()
            this.newTodo.CurrentDateTime = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate() + ' ' + today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
            this.$validator.validateAll('form-add-todo').then(function (result) {
                if (result) {
                    app.$http.post('/api/TodoApi/AddTodo', app.newTodo).then(
                        function success(response) {
                        },
                        function error() {
                            console.log('Todo cannot be added!');
                        })
                }
            });
        },

        GetItemDetails: function (todo) {
            this.$http.post('/api/TodoApi/GetTodoById', todo.Id).then(
                function success(response) {
                },
                function error() {
                    console.log('Nu merge');
                })
        },

        Rewrite: function (todoCopy) {
            this.$validator.validateAll('form-change-todo').then(function (result) {
                if (result) {
                    app.$http.post('/api/TodoApi/UpdateTodo', todoCopy).then(
                        function success(response) {
                            Toast.fire({
                                title: 'Todo has been Updated!',
                            })
                            $('#createServices').modal('hide');
                            app.GetDefaultCatalog();
                            app.loadingPage = false;
                        },
                        function error() {
                            Toast.fire({
                                title: 'Todo cannot be Updated!',
                            })
                            app.loadingPage = false;
                        })
                }
            });
        },

        DeleteItem: function (todo) {
            this.$http.post('/api/TodoApi/DeleteTodo', todo.Id).then(
                function success(response) {
                    const index = app.todos.indexOf(todo);
                    app.todos.splice(index, 1);
                },
                function error() {
                    console.log('Item cannnot be deleted!');
                })
        },

        GetDoneTodos: function (pageDone) {
            if (!pageDone) {
                pageDone = 1;
            }
            this.filterDone.CurrentPage = pageDone;
            this.$http.get('/api/TodoApi/GetDoneTodos?CurrentPage=' + this.filterDone.CurrentPage + '&PageSize=' + this.filterDone.PageSize).then(response => {
                // get body data
                this.doneTodos = response.body.Items;
                this.nrOfPagesDone = response.body.NumberOfPages;
            }, response => {
                // error callback
            });
        },

        GetToBeDoneTodos: function (page) {
            if (!page) {
                page = 1;
            }
            this.filterToBeDone.CurrentPage = page;
            this.$http.get('/api/TodoApi/GetToBeDoneTodos?CurrentPage=' + this.filterToBeDone.CurrentPage + '&PageSize=' + this.filterToBeDone.PageSize).then(response => {
                // get body data
                this.toBeDoneTodos = response.body.Items;
                this.nrOfPagesToBeDone = response.body.NumberOfPages;
            }, response => {
                // error callback
            });
        },

        GetPrioritys: function () {
            this.$http.get('/api/PriorityApi/GetPrioritys').then(response => {
                // get body data
                this.priorities = response.body;
            }, response => {
                // error callback
            });
        },

        SelectPriority: function (priority) {
            this.selectedPriority = priority;
            this.newTodo.PriorityId = priority.Id;
        },

        GetUsers: function () {
            this.$http.get('/api/UsersApi/GetUsers').then(response => {
                // get body data
                this.users = response.body;
            }, response => {
                // error callback
            });
        },

        SelectUser: function (user) {
            this.selectedUser = user;
            this.newTodo.UserId = user.Id;
        },
    },
    created: function () {
        this.GetTodos();
        this.GetDoneTodos();
        this.GetToBeDoneTodos();
        this.GetPrioritys();
        this.GetUsers();
    }
})

