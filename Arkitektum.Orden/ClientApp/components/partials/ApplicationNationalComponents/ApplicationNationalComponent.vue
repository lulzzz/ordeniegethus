<template>

    <!-- Saved application national component -->
    <div v-if="saved" class="list-group-item d-flex justify-content-between align-items-center">
        <a>{{ applicationNationalComponent.name}}</a>
        <div class="float-right" v-if="writeAccess">
            <span class="btn btn-outline-danger" v-on:click="$emit('remove')"><span class="fas fa-trash"></span></span>
        </div>
    </div>

    <!-- New application national component -->
    <div v-else class="list-group-item">
        <div class="form-group">
            <filter-select 
                v-bind:options="availableNationalComponents"
                v-bind:selected-options="selectedApplicationNationalComponents" 
                v-bind:filterable-properties="['name']"
                primary-text-property="name"
                secondary-text-property="id"
                value-property="id"
                placeholder="Søk etter nasjonale felleskomponenter"
                v-on:select="selectApplicationNationalComponent($event)">
                </filter-select>
        </div>
        <div class="float-right">
            <span class="btn btn-outline-danger" v-on:click="$emit('remove')"><span class="fas fa-trash"></span> Fjern</span>
            <span class="btn btn-outline-success" v-on:click="addApplicationNationalComponentToApplication()"><span class="fas fa-check"></span> Legg til</span>
        </div>
    </div>

</template>

<style src="./ApplicationNationalComponent.scss"></style>
<script src="./ApplicationNationalComponent"></script>
