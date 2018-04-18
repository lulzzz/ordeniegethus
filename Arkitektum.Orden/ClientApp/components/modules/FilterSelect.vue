<template>
    <div v-click-outside="hideResults">
        <input v-model="inputValue" v-on:click="showResults" v-on:keyup="updateFilterResult($event.target.value)" placeholder="Søk etter tjenesteområde" class="form-control" />
        <div class="filter-results" v-show="active" v-bind:class="{active: active}">
            <div v-for="filterResult in filterResults" v-bind:key="filterResult.id" class="filter-results-item" v-on:click="selectFilterResult(filterResult)">
                <span class="primary-text-property">{{ filterResult[primaryTextProperty] }}</span>
                <span v-if="secondaryTextProperty !== undefined" class="secondary-text-property">{{ filterResult[secondaryTextProperty] }}</span>
            </div>
        </div>
    </div>
</template>

<script src="./FilterSelect.js"></script>
<style lang="scss" src="./FilterSelect.scss"></style>
