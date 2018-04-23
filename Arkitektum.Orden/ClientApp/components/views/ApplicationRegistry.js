import FilterSelect from '../modules/FilterSelect.vue';

export default {
    name: 'ApplicationRegistry',
    data() {
        return {
            apiUrls: {
                get: `/appregistry/all`,
                create: `/appregistry/create`
            },
            application: {},
            availableApplications: [],
            url: {}
        }
    },
    components: {
        FilterSelect
    },
    mounted() {
        this.getAvailableApplications();
    },
    methods: {
        getAvailableApplications() {
            Promise.resolve(this.$root.getApiData(this.apiUrls.get))
                .then((apiData) => {
                    this.availableApplications = apiData;
                });
        },
        selectApplication(application) {
            this.application = application;
        },
        createApplication() {
            Promise.resolve(this.$root.postApiData(this.apiUrls.create, 
                    { commonApplicationId: this.application.id, versionNumber: this.application.versions[0].versionNumber}))
                .then((apiData) => {
                    console.log(apiData);
                });
        }
    },
}

