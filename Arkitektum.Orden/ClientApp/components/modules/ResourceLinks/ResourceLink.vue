<template>

    <!-- Saved resource link-->
    <div v-if="saved" class="list-group-item">
        <div v-show="editable">
            <div class="input-group">
                <input v-model="data.description" placeholder="Beskrivelse" class="form-control" />
                <input v-model="data.url" placeholder="URL" class="form-control" />
                <div class="input-group-append">
                    <span class="btn btn-outline-secondary" v-on:click="editable = !editable"><span class="fas fa-times"></span> Avbryt</span>
                    <span class="btn btn-outline-success" v-on:click="$parent.updateResourceLink(resourceLink.id, data)"><span class="fas fa-check"></span> Oppdater</span>
                </div>
            </div>
        </div>
        <div v-show="!editable">
            <a v-bind:href="resourceLink.url">{{ resourceLink.description}}</a>
            <div class="btn-group float-right">
                <span class="btn btn-outline-secondary" v-on:click="editable = !editable"><span class="fas fa-edit"></span> Rediger</span>
                <span class="btn btn-outline-danger" v-on:click="editable = !editable"><span class="fas fa-trash"></span> Fjern</span>
            </div>
        </div>
    </div>

    <!-- New resource link-->
    <div v-else class="list-group-item">
        <div class="input-group">
            <input v-model="data.description" placeholder="Beskrivelse" class="form-control" />
            <input v-model="data.url" placeholder="URL" class="form-control" />
            <div class="input-group-append">
                <span class="btn btn-outline-danger" v-on:click="$emit('remove')"><span class="fas fa-trash"></span> Fjern</span>
                <span class="btn btn-outline-success" v-on:click="$parent.postResourceLink(data)"><span class="fas fa-check"></span> Legg til</span>
            </div>
        </div>
    </div>

</template>

<style src="./ResourceLink.scss"></style>
<script src="./ResourceLink"></script>
