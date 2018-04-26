<template>

    <!-- Saved application dataset -->
    <div v-if="saved" class="list-group-item d-flex justify-content-between align-items-center">
        <a v-bind:href="datasetDetailsPageUrl()">{{ applicationDataset.name}}</a>
        <div class="float-right" v-if="writeAccess">
            <span class="btn btn-outline-danger" v-on:click="$emit('remove')"><span class="fas fa-trash"></span></span>
        </div>
    </div>

    <!-- New application dataset -->
    <div v-else class="list-group-item">
        <div class="form-group">
            <filter-select 
                v-bind:options="availableDatasets"
                v-bind:selected-options="selectedApplicationDatasets"
                v-bind:filterable-properties="['name']"
                primary-text-property="name"
                value-property="id"
                placeholder="Søk etter datasett"
                v-on:select="selectApplicationDataset($event)">
                </filter-select>
        </div>
        <div class="float-right">
            <span class="btn btn-outline-danger" v-on:click="$emit('remove')"><span class="fas fa-trash"></span> Fjern</span>
            <span class="btn btn-outline-success" v-on:click="$parent.postApplicationDataset(data)"><span class="fas fa-check"></span> Legg til</span>
        </div>
    </div>

</template>

<style src="./ApplicationDataset.scss"></style>
<script src="./ApplicationDataset"></script>
