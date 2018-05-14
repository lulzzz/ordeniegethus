/* Components */
import ApplicationSuperUser from './ApplicationSuperUsers/ApplicationSuperUser.vue';

export default {
    name: 'ApplicationSuperUsers',
    props: {
        applicationSuperUsers: Array,
        applicationId: String,
        organizationId: String,
        route: String,
        writeAccess: {
            type: Boolean,
            default: false
        }
    },
    components: {
        ApplicationSuperUser
    },
    data() {
        return {
            apiData: null,
            availableSuperUsers: [],          
            newApplicationSuperUser: false,
        }
    },
    mounted() {
       // this.apiData = this.applicationSuperUsers;
       this.getApplicationSuperUsers();
    },
    watch: {
        apiData() {
            if (this.apiData) {
                this.getAvailableSuperUsers();
            }
        }
    },
    methods: {
        createNewApplicationSuperUser() {
            this.newApplicationSuperUser = true;
        },
        removeNewApplicationSuperUser() {
            this.newApplicationSuperUser = false;
        },
        getApplicationSuperUsers() {
            Promise.resolve(this.$root.getApiData(`${this.route}/${this.applicationId}/SuperUsers`))
                .then((apiData) => {
                    this.apiData = apiData;
                });
        },
        getAvailableSuperUsers() {
            let availableSuperUsers = [];
            Promise.resolve(this.$root.getApiData(`/SuperUsers/organization/${this.organizationId}`))
            .then((apiData) => {
                apiData.forEach(superUser => {
                    if (!this.apiData.filter(d => d.id == superUser.id).length) {
                        availableSuperUsers.push(superUser);
                    }
                });
                this.availableSuperUsers = availableSuperUsers;
            });
        },
        postApplicationSuperUser(superUser) {
            Promise.resolve(this.$root.postApiData(`${this.route}/${this.applicationId}/SuperUsers/`, superUser ))
                .then(() => {
                    this.getApplicationSuperUsers();
                    this.removeNewApplicationSuperUser();
                });
        },
        removeApplicationSuperUser(superUserId) {
            Promise.resolve(this.$root.deleteApiData(`${this.route}/${this.applicationId}/SuperUsers/${superUserId}`))
                .then(() => {
                    this.getApplicationSuperUsers();
                });
        }
    }
}

