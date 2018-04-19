/* Components */
import ApplicationSector from './ApplicationSectors/ApplicationSector.vue';

export default {
    name: 'ApplicationSectors',
    props: ['applicationSectors', 'applicationId'],
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
            Promise.resolve(this.$root.postApiData(`/sectors/application/`, { sectorId: data.id, applicationId: this.applicationId }))
                .then(() => {
                    this.getApplicationSectors();
                    this.removeNewApplicationSector();
                });
        },
        removeApplicationSector(sectorId) {
            Promise.resolve(this.$root.deleteApiData(`/sectors/application/${sectorId}/${this.applicationId}`))
                .then(() => {
                    this.getApplicationSectors();
                });
        }
    }
}

