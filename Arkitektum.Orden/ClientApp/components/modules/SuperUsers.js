/* Components */
import SuperUser from './SuperUsers/SuperUser.vue';

export default {
    name: 'SuperUsers',
    props: ['apiUrl'],
    components: {
        SuperUser
    },
    data() {
        return {
            savedSuperUsers: null,
            newSuperUser: false
        }
    },
    mounted() {
        this.getSuperUsers();
    },
    methods: {
        getSuperUsers() {
            Promise.resolve(this.$root.getApiData(this.apiUrl))
                .then((apiData) => {
                    this.savedSuperUsers = apiData;
                });
        },
        createNewSuperUser() {
            this.newSuperUser = true;
        },
        removeNewSuperUser() {
            this.newSuperUser = false;
        },
        postSuperUser(data) {
            Promise.resolve(this.$root.postApiData(this.apiUrl, data))
                .then(() => {
                    this.getSuperUsers();
                    this.removeNewSuperUser();
                });
        },
        updateSuperUser(superUserId, data) {
            Promise.resolve(this.$root.putApiData(`${this.apiUrl}/${superUserId}`, data))
                .then(() => {
                    this.getSuperUsers();
                    this.removeNewSuperUser();
                });
        }
    }
}

