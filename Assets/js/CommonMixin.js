
var commonMixin = {
    data: {
        navCustomersList: [],
        navSelectedCustomer: {},
        token: '',
        selectedPage: 'Device managament',
    },
    methods: {
        GetAllCustomerss: function () {
            this.$http.get('/api/Customers/GetAllCustomers',
                {
                    headers: {
                        Authorization: this.token
                    }
                }).then(
                    function success(response) {
                        this.navCustomersList = response.body;
                        //this.navCustomersList.push({ Id: '-1', Name: 'All' });
                        if (this.navCustomersList.length > 0) {
                            var localCustomer = localStorage.getItem("customer");
                            if (localCustomer != null && localCustomer > 0) {
                                this.navCustomersList.forEach(function (cust) {
                                    if (cust.Id == localCustomer) {
                                        app.navSelectedCustomer = cust;
                                    }
                                })
                            } else {
                                this.navSelectedCustomer = this.navCustomersList[0];
                                localStorage.setItem("customer", this.navCustomersList[0].Id);
                            }
                        }
                    },
                )
        },

        SelectNavCustomer: function (customer) {
            this.navSelectedCustomer = customer;
            localStorage.setItem("customer", customer.Id);
            //this.PageActions(true);
        },
        GetAuthorizationToken: function () {
            var token = localStorage.getItem("accessToken");
            if (token) {
                return 'Bearer ' + token;
            }
            return null;
        },

        currentRoute() {
            this.$nextTick(() => {
                this.selectedPage = this.$route.name
            });
            console.log(this.selectedPage)
        },

        SelectThePage: function (click) {
            if (click.target.tagName  == 'SPAN') {
                let value = click.target.outerText;
                //outerText = page name
                localStorage.setItem('currentPage',value);
            }
        },

        CheckTheCurrentPage: function () {
            var currentPage = localStorage.getItem("currentPage");
            if (currentPage != null) {
                this.selectedPage = currentPage;
            }
        },
    },
    created: function () {
        this.token = this.GetAuthorizationToken();
        this.GetAllCustomerss();
        this.CheckTheCurrentPage();
        console.log(this.selectedPage);

    },

};