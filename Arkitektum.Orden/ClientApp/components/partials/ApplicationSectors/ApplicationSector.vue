<template>

    <!-- Saved application sector -->
    <div v-if="saved" class="list-group-item d-flex justify-content-between align-items-center">
        <a>{{ applicationSector.sectorName}}</a>
        <div class="float-right">
            <span class="btn btn-outline-danger" v-on:click="$emit('remove')"><span class="fas fa-trash"></span></span>
        </div>
    </div>

    <!-- New application sector -->
    <div v-else class="list-group-item">
        <div class="form-group">
            <filter-select 
                v-bind:options="availableApplicationSectors" 
                v-bind:filterable-properties="['name']"
                primary-text-property="name"
                secondary-text-property="id"
                value-property="id"
                v-on:select="selectApplicationSector($event)">
                </filter-select>
        </div>
        <div class="float-right">
            <span class="btn btn-outline-danger" v-on:click="$emit('remove')"><span class="fas fa-trash"></span> Fjern</span>
            <span class="btn btn-outline-success" v-on:click="$parent.postApplicationSector(data)"><span class="fas fa-check"></span> Legg til</span>
        </div>
    </div>

</template>

<style src="./ApplicationSector.scss"></style>
<script src="./ApplicationSector"></script>
