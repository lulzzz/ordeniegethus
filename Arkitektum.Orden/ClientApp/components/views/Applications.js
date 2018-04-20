export default {
    name: 'Applications',
    props: ['sectorId'],
    data() {
        return {
            apiData:null,
        }
    },
    methods: {
        getApiData() {
            Promise.resolve(this.$root.getApiData('/Applications/secto'))
        }
    }
  
}
