/* Components */
import ApplicationStandard from './ApplicationStandards/ApplicationStandard.vue';

export default {
    name: 'ApplicationStandards',
    props: {
        applicationStandards: Array,
        applicationId: String,
        writeAccess: {
            type: Boolean,
            default: false
        }
    },
    components: {
        ApplicationStandard
    },
    data() {
        return {
            apiData: null,            
            newApplicationStandard: false,
            availableStandards: []
        }
    },
    mounted() {
        this.apiData = this.applicationStandards;
    },
    watch: {
        apiData() {
            if (this.apiData) {
                this.getAvailableStandards();
            }
        }
    },
    methods: {
        createNewApplicationStandard() {
            this.newApplicationStandard = true;
        },
        removeNewApplicationStandard() {
            this.newApplicationStandard = false;
        },
        getApplicationStandards() {
            Promise.resolve(this.$root.getApiData(`/api/standards/application/${this.applicationId}`))
                .then((apiData) => {
                    this.apiData = apiData;
                });
        },
        getAvailableStandards() {
            let availableStandards = [];
            Promise.resolve(this.$root.getApiData(`/api/standards`))
            .then((apiData) => {
                apiData.forEach(Standard => {
                    if (!this.apiData.filter(nC => nC.id == Standard.id).length) {
                        availableStandards.push(Standard);
                    }
                });
                this.availableStandards = availableStandards;
            });
        },
        postApplicationStandard(data) {
            Promise.resolve(this.$root.postApiData(`/api/standards/application/`, data))
                .then(() => {
                    this.getApplicationStandards();
                    this.removeNewApplicationStandard();
                });
        },
        removeApplicationStandard(applicationStandardId) {
            Promise.resolve(this.$root.deleteApiData(`/api/standards/${applicationStandardId}/application/${this.applicationId}`))
                .then(() => {
                    this.getApplicationStandards();
                });
        }
    }
}

