<template>
    <div>
        <input v-on:keyup="updateFilterResult($event.target.value)" placeholder="Søk etter tjenesteområde" class="form-control" />
        <div class="filter-results">
            <div v-for="filterResult in filterResults" class="filter-results-item" v-on:click="$emit('select', filterResult)">
                <span class="primary-text-property">{{ filterResult[primaryTextProperty] }}</span>
                <span v-if="secondaryTextProperty !== undefined" class="secondary-text-property">{{ filterResult[secondaryTextProperty] }}</span>
            </div>
        </div>
    </div>
</template>

<script src="./FilterSelect.js"></script>
<style src="./FilterSelect.scss"></style>