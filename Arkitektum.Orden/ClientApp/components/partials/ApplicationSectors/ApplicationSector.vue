<template>

    <!-- Saved application sector -->
    <div v-if="saved" class="list-group-item d-flex justify-content-between align-items-center">
        <a v-bind:href="sectorDetailsPageUrl()">{{ applicationSector.name }}</a>
        <div class="float-right" v-if="writeAccess">
            <span class="btn btn-outline-danger" v-on:click="$emit('remove')"><span class="fas fa-trash"></span></span>
        </div>
    </div>

    <!-- New application sector -->
    <div v-else>
        <filter-select 
            v-bind:options="availableSectors"
            v-bind:filterable-properties="['name']"
            primary-text-property="name"
            value-property="id"
            placeholder="Søk etter tjenesteområde"
            v-on:select="selectApplicationSector($event)"
            class="list-group-item-input-field">
        </filter-select>
        <div class="row">
            <div class="col-sm-6">
                <span class="btn btn-outline-danger list-group-item-button" v-on:click="$emit('remove')"><span class="fas fa-trash"></span> Fjern</span>
            </div>
            <div class="col-sm-6">
                <span class="btn btn-outline-success list-group-item-button" v-on:click="$parent.postApplicationSector(data)"><span class="fas fa-check"></span> Legg til</span>
            </div>
        </div>
    </div>

</template>

<style src="./ApplicationSector.scss"></style>
<script src="./ApplicationSector"></script>
