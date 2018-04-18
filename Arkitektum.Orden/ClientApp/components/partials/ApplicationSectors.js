/* Components */
import ApplicationSector from './ApplicationSectors/ApplicationSector.vue';

export default {
    name: 'ApplicationSectors',
    props: ['applicationSectors', 'applicationId', 'availableApplicationSectors'],
    components: {
        ApplicationSector
    },
    data() {
        return {
            apiData: null,            
            newApplicationSector: false,
        }
    },
    mounted() {
        this.apiData = this.applicationSectors;
    },
    methods: {
        createNewApplicationSector() {
            this.newApplicationSector = true;
        },
        removeNewApplicationSector() {
            this.newApplicationSector = false;
        },
        getApplicationSectors() {
            Promise.resolve(this.$root.getApiData(`/sectors/application/${this.applicationId}`))
                .then((apiData) => {
                    this.apiData = apiData;
                });
        },
        postApplicationSector(data) {
            Promise.resolve(this.$root.postApiData(`/sectors/application/`, data))
                .then(() => {
                    this.getApplicationSectors();
                    this.removeNewApplicationSector();
                });
        },
        removeApplicationSector(applicationSectorId) {
            Promise.resolve(this.$root.deleteApiData(`/sectors/application/`, { id: applicationSectorId }))
                .then(() => {
                    this.getApplicationSectors();
                });
        }
    }
}

