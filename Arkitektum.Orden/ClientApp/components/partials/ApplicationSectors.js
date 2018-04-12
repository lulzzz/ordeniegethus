/* Components */
import ApplicationSector from './ApplicationSectors/ApplicationSector.vue';

export default {
    name: 'ApplicationSectors',
    props: ['apiData', 'applicationId', 'availableApplicationSectors'],
    components: {
        ApplicationSector
    },
    data() {
        return {
            newApplicationSector: false,
            apiUrls: {
                post: `/sectors/application/`,
                delete: `/sectors/application/`
            }
        }
    },
    methods: {
        createNewApplicationSector() {
            this.newApplicationSector = true;
        },
        removeNewApplicationSector() {
            this.newApplicationSector = false;
        },
        postApplicationSector(data) {
            Promise.resolve(this.$root.postApiData(this.apiUrls.post, data))
                .then(() => {
                    this.getApplicationSectors();
                    this.removeNewApplicationSector();
                });
        },
        removeApplicationSector(applicationSectorId) {
            Promise.resolve(this.$root.deleteApiData(this.apiUrls.delete, { id: applicationSectorId }))
                .then(() => {
                    this.getApplicationSectors();
                });
        }
    }
}

