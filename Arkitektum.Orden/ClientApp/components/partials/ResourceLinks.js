/* Components */
import ResourceLink from './ResourceLinks/ResourceLink.vue';

export default {
    name: 'ResourceLinks',
    props: {
        applicationId: String,
        writeAccess: {
            type: Boolean,
            default: false
        }
    },
    components: {
        ResourceLink
    },
    data() {
        return {
            apiData: null,
            newResourceLink: false,
        }
    },
    mounted() {
        this.getResourceLinks();
    },
    methods: {
        getResourceLinks() {
            Promise.resolve(this.$root.getApiData(`/ResourceLinks/Application/${this.applicationId}`))
                .then((apiData) => {
                    this.apiData = apiData;
                });
        },
        createNewResourceLink() {
            this.newResourceLink = true;
        },
        removeNewResourceLink() {
            this.newResourceLink = false;
        },
        postResourceLink(data) {
            Promise.resolve(this.$root.postApiData(`/ResourceLinks/Application/${this.applicationId}`, data))
                .then(() => {
                    this.getResourceLinks();
                    this.removeNewResourceLink();
                });
        },
        updateResourceLink(resourceLinkId, data) {
            Promise.resolve(this.$root.putApiData(`/ResourceLinks/Application/${this.applicationId}/${resourceLinkId}`, data));
        },
        removeResourceLink(resourceLinkId) {
            Promise.resolve(this.$root.postApiData(`/ResourceLinks/Delete`, { id: resourceLinkId }))
                .then(() => {
                    this.getResourceLinks();
                });
        }
    }
}

