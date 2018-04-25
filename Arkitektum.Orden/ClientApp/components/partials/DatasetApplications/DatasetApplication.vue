<template>

    <!-- Saved dataset application -->
    <div v-if="saved" class="list-group-item d-flex justify-content-between align-items-center">
        <a v-bind:href="applicationDetailsPageUrl()">{{ datasetApplication.name}}</a>
        <div class="float-right" v-if="writeAccess">
            <span class="btn btn-outline-danger" v-on:click="$emit('remove')"><span class="fas fa-trash"></span></span>
        </div>
    </div>

    <!-- New dataset application -->
    <div v-else class="list-group-item">
        <div class="form-group">
            <filter-select 
                v-bind:options="availableApplications"
                v-bind:selected-options="selectedDatasetApplications"
                v-bind:filterable-properties="['name']"
                primary-text-property="name"
                secondary-text-property="id"
                value-property="id"
                placeholder="Søk etter applikasjon"
                v-on:select="selectDatasetApplication($event)">
                </filter-select>
        </div>
        <div class="float-right">
            <span class="btn btn-outline-danger" v-on:click="$emit('remove')"><span class="fas fa-trash"></span> Fjern</span>
            <span class="btn btn-outline-success" v-on:click="$parent.postDatasetApplication(data)"><span class="fas fa-check"></span> Legg til</span>
        </div>
    </div>

</template>

<style src="./DatasetApplication.scss"></style>
<script src="./DatasetApplication"></script>
