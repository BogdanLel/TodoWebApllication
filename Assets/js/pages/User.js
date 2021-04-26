Vue.use(VueResource);

var app = new Vue({
    el: '#app',
    data: {
        users: [],
        todos: [],
        displayTodo: false,
        selectedRole: {},
        roles: [],
        assignRole: {},
    },
    methods: {
        GetUsers: function () {
            this.$http.get('/api/UsersApi/GetUsers').then(response => {
                // get body data
                this.users = response.body;
                this.users.forEach(user => user.Plus = true)
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

        GetRoles: function () {
            this.$http.get('/api/RoleApi/GetRoles').then(response => {
                // get body data
                this.roles = response.body;
            }, response => {
                // error callback
            });
        },

        SelectRole: function (role) {
            this.selectedRole = role;
        },

        AssignRole: function (id, role) {
            this.assignRole.Name = role;
            this.assignRole.Id = id;
            this.$http.post('/Account/AssignRole', this.assignRole).then(function (response) {
                Swal.fire({
                    icon: 'success',
                    title: 'User role has been assigned',
                    showConfirmButton: false,
                    timer: 1500
                })
            });
        },

        DeleteUser: function (user) {
            Swal.fire({
                title: 'Are you sure?',
                text: "You won't be able to revert this!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, delete it!'
            }).then((result) => {
                //const index = app.users.indexof(user);
                //app.users.splice(index, 1);
                if (result.isConfirmed) {
                    this.$http.delete('/api/UsersApi/DeleteUser?userId=' + user.Id).then(
                        function success(response) {
                            Swal.fire(
                                'Deleted!',
                                'Your user has been deleted.',
                                'success'
                            )
                        },
                        response => {
                            // error callback
                        });
                }
            })
        },

        Test: function () {
            Swal.fire({
                title: 'Error!',
                text: 'Do you want to continue',
                icon: 'error',
                confirmButtonText: 'Cool'
            })
        }
    },
    created: function () {
        this.GetUsers();
        this.GetRoles();
    }
})

