/* Components */
import ApplicationSector from './ApplicationSectors/ApplicationSector.vue';

export default {
    name: 'ApplicationSectors',
    props: {
        applicationSectors: Array,
        applicationId: String,
        writeAccess: {
            type: Boolean,
            default: false
        }
    },
    components: {
        ApplicationSector
    },
    data() {
        return {
            apiData: null,            
            newApplicationSector: false,
            availableSectors: []
        }
    },
    mounted() {
        this.apiData = this.applicationSectors;
    }, 
    watch: { 
        apiData() { 
            if (this.apiData) {
                this.getAvailableSectors(); 
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
        getApplicationSectors() {
            Promise.resolve(this.$root.getApiData(`/sectors/application/${this.applicationId}`))
                .then((apiData) => {
                    this.apiData = apiData;
                });
        },
        getAvailableSectors() { 
            let availableSectors = []; 
            Promise.resolve(this.$root.getApiData(`/sectors/all`)) 
            .then((apiData) => { 
                apiData.forEach(sector => { 
                    if (!this.apiData.filter(s => s.id == sector.id).length) { 
                        availableSectors.push(sector); 
                    } 
                }); 
                this.availableSectors = availableSectors; 
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

