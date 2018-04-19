export default {
  name: "FilterSelect",
  props: [
    "options",
    "selectedOptions",
    "filterableProperties",
    "primaryTextProperty",
    "secondaryTextProperty",
    "valueProperty",
    "minSearchLength",
    "placeholder"
  ],
  data() {
    return {
      filterResults: [],
      inputValue: "",
      active: false
    };
  },
  watch: {
    options() {
      if (!this.minSearchLength) {
        this.updateFilterResult("");
      }
    },
    selectedOptions() {
      if (!this.minSearchLength) {
        this.updateFilterResult("");
      }
    }
  },
  methods: {
    updateFilterResult(filterValue) {
      let filterResults = [];
      let unselectedOptions = this.getUnselectedOptions();
      let minSearchLength =
        this.minSearchLength !== undefined ? this.minSearchLength : 0;
      if (!filterValue.length) {
        filterResults = unselectedOptions;
      } else if (filterValue.length >= minSearchLength) {
        unselectedOptions.forEach(option => {
          let optionHasMatch = false;
          this.filterableProperties.forEach(filterableProperty => {
            if (
              option[filterableProperty]
                .toUpperCase()
                .indexOf(filterValue.toUpperCase()) > -1
            ) {
              optionHasMatch = true;
            }
          });

          if (optionHasMatch) {
            filterResults.push(option);
          }
        });
      }
      this.filterResults = filterResults;
    },
    optionIsSelectedById(option) {
      let optionIsSelected = false;
      if (this.selectedOptions) {
        this.selectedOptions.forEach(selectedOption => {
          if (option.id == selectedOption.id) {
            optionIsSelected = true;
            return;
          }
        });
      }
      return optionIsSelected;
    },
    getUnselectedOptions() {
      let selectedOptions =
        this.selectedOptions !== undefined ? this.selectedOptions : [];
      let unselectedOptions = [];
      this.options.forEach(option => {
        if (!this.optionIsSelectedById(option)) {
          unselectedOptions.push(option);
        }
      });
      return unselectedOptions;
    },
    selectFilterResult(filterResult) {
      this.inputValue = filterResult[this.primaryTextProperty];
      this.$emit("select", filterResult);
      this.hideResults();
    },
    showResults() {
      this.active = true;
    },
    hideResults() {
      this.active = false;
    }
  },
  directives: {
    "click-outside": {
      bind: function(el, binding, vNode) {
        // Define Handler and cache it on the element
        const bubble = binding.modifiers.bubble;
        const handler = e => {
          if (bubble || (!el.contains(e.target) && el !== e.target)) {
            binding.value(e);
          }
        };
        el.__vueClickOutside__ = handler;

        // Add Event Listeners
        document.addEventListener("click", handler);
      },

      unbind: function(el, binding) {
        // Remove Event Listeners
        document.removeEventListener("click", el.__vueClickOutside__);
        el.__vueClickOutside__ = null;
      }
    }
  }
};
