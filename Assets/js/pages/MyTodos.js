Vue.use(VeeValidate, {
    classes: true,
    events: 'change',
    classNames: {
        valid: 'is-valid',
        invalid: 'is-invalid'
    }
});

Vue.use(VueResource);
const Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 3000,
    timerProgressBar: true,
    didOpen: (toast) => {
        toast.addEventListener('mouseenter', Swal.stopTimer)
        toast.addEventListener('mouseleave', Swal.resumeTimer)
    }
})
var app = new Vue({
    el: '#app',
    data: {
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
        todoEdit: {},
        file: '',
        fileName: '',
        fileEntity: {}
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

        GetMyTodos: function (page, order) {
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
            this.$http.post('/api/TodoApi/GetMyTodos', this.filter).then(response => {
                // get body data
                this.todos = response.body.Items;
                this.nrOfPages = response.body.NumberOfPages;
            }, response => {
                // error callback
            });
        },

        AddMyTodo: function () {
            const today = new Date()
            this.newTodo.CurrentDateTime = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate() + ' ' + today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
            this.$validator.validateAll('form-add-todo').then(function (result) {
                if (result) {
                    app.$http.post('/api/TodoApi/AddMyTodo', app.newTodo).then(
                        function success(response) {
                            Swal.fire({
                                icon: 'success',
                                title: 'Task has been added',
                                showConfirmButton: false,
                                timer: 1500
                            })
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
                    this.todoEdit = response.body;
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
                                icon: 'success',
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
            Swal.fire({
                title: 'Are you sure?',
                text: "You won't be able to revert this!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, delete it!'
            }).then((result) => {
                if (result.isConfirmed) {
                    this.$http.post('/api/TodoApi/DeleteTodo', todo.Id).then(
                        function success(response) {
                            const index = app.todos.indexOf(todo);
                            app.todos.splice(index, 1);
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

        GetMyDoneTodos: function (pageDone) {
            if (!pageDone) {
                pageDone = 1;
            }
            this.filterDone.CurrentPage = pageDone;
            this.$http.get('/api/TodoApi/GetMyDoneTodos?CurrentPage=' + this.filterDone.CurrentPage + '&PageSize=' + this.filterDone.PageSize).then(response => {
                // get body data
                this.doneTodos = response.body.Items;
                this.nrOfPagesDone = response.body.NumberOfPages;
            }, response => {
                // error callback
            });
        },

        GetMyToBeDoneTodos: function (page) {
            if (!page) {
                page = 1;
            }
            this.filterToBeDone.CurrentPage = page;
            this.$http.get('/api/TodoApi/GetMyToBeDoneTodos?CurrentPage=' + this.filterToBeDone.CurrentPage + '&PageSize=' + this.filterToBeDone.PageSize).then(response => {
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

        OnFileChange: function () {
            var input = event.target;
            if (input.files && input.files[0]) {
                var fileName = input.files[0].name
                var reader = new FileReader();
                if (input.files[0].size > 100000000) { //1 mb
                    swal({
                        position: 'top-end',
                        type: 'error',
                        title: '@Resources.Timesheet_UI.file_is_to_big',
                        text: "@Resources.Timesheet_UI.maxim_size_mb",
                        showConfirmButton: false,
                        timer: 3000
                    })
                }
                else {
                    reader.onload = function (e) {
                        app.file = e.target.result;
                        app.FileExtValidate(fileName);
                    }
                    reader.readAsDataURL(input.files[0]);
                }
            }
        },

        FileExtValidate: function (fileName) {
            var validExt = ".png .jpg .jpeg .jfif .docx .pdf";
            var filePath = fileName;
            var getFileExt = filePath.substring(filePath.lastIndexOf('.') + 1).toLowerCase();
            var pos = validExt.indexOf(getFileExt);
            if (pos > 0) {
                this.fileName = fileName;
                this.$forceUpdate();
                return true;
            }
            else {
                swal({
                    position: 'top-end',
                    type: 'error',
                    title: '@Resources.Timesheet_UI.this_file_is_not_allowed',
                    showConfirmButton: false,
                    timer: 3000,

                })
                return false;
            }
        },

        UploadFile: function (todoId) {
            this.fileEntity.FileBase64 = this.file;
            this.fileEntity.FileName = this.fileName;
            this.fileEntity.TodoId = todoId;
            this.$http.post('/api/FileApi/UploadFile', this.fileEntity).then(response => {
                // response body
            }, response => {
                // error callback
            });
        },

        DownloadFile: function (fileId) {
            this.$http.get('/api/FileApi/DownloadFile?fileId=' + fileId).then(response => {
                console.log(response.body);
                console.log(this.ConvertBase64ToFile(response.body))
            }, response => {
                // error callback
            });
        },

        ConvertBase64ToFile: function (image) {
            const byteString = atob(image.split(',')[1]);
            const ab = new ArrayBuffer(byteString.length);
            const ia = new Uint8Array(ab);
            for (let i = 0; i < byteString.length; i += 1) {
                ia[i] = byteString.charCodeAt(i);
            }
            const newBlob = new Blob([ab], {
                type: 'image/jpeg',
            });
            return newBlob;
        },

    },
    created: function () {
        this.GetMyTodos();
        this.GetMyDoneTodos();
        this.GetMyToBeDoneTodos();
        this.GetPrioritys();
    }
})

