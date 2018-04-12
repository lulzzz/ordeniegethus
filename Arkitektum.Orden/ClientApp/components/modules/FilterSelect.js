export default {
    name: 'FilterSelect',
    props: ['options', 'filterableProperties', 'primaryTextProperty', 'secondaryTextProperty', 'valueProperty'],
    data() {
        return {
            filterResults: []
        }
    },
    methods: {
        updateFilterResult(filterValue) {
            let filterResults = [];
            if (filterValue.length){
                this.options.forEach((option) => {
                    let optionHasMatch = false;
                    this.filterableProperties.forEach((filterableProperty) => {
                        if (option[filterableProperty].toUpperCase().indexOf(filterValue.toUpperCase()) > -1){
                            optionHasMatch = true;
                        }
                    });

                    if (optionHasMatch) {
                        filterResults.push(option);
                    }
                });
            }
            this.filterResults = filterResults;
        }
    }
}

