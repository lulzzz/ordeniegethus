<template>

    <!-- Saved super user-->
    <div v-if="saved" class="list-group-item">
        <div v-show="editable">
            <div class="input-group">
                <input v-model="data.name" placeholder="Navn" class="form-control" />
                <input v-model="data.email" placeholder="E-post" class="form-control" />
                <div class="input-group-append">
                    <span class="btn btn-outline-secondary" v-on:click="editable = !editable"><span class="fas fa-times"></span> Avbryt</span>
                    <span class="btn btn-outline-success" v-on:click="$parent.updateSuperUser(superUser.id, data); editable = !editable"><span class="fas fa-check"></span> Oppdater</span>
                </div>
            </div>
        </div>
        <div v-show="!editable">
            <a v-bind:href="emailAsLink(superUser.email)">{{ superUser.name}} &lt;{{ superUser.email }}&gt;</a>
            <div class="btn-group float-right">
                <span class="btn btn-outline-secondary" v-on:click="editable = !editable"><span class="fas fa-edit"></span> Rediger</span>
                <span class="btn btn-outline-danger" v-on:click="editable = !editable"><span class="fas fa-trash"></span> Fjern</span>
            </div>
        </div>
    </div>

    <!-- New super user-->
    <div v-else class="list-group-item">
        <div class="input-group">
            <input v-model="data.name" placeholder="Navn" class="form-control" />
            <input v-model="data.email" placeholder="E-post" class="form-control" />
            <div class="input-group-append">
                <span class="btn btn-outline-danger" v-on:click="$emit('remove')"><span class="fas fa-trash"></span> Fjern</span>
                <span class="btn btn-outline-success" v-on:click="$parent.postSuperUser(data)"><span class="fas fa-check"></span> Legg til</span>
            </div>
        </div>
    </div>

</template>

<style src="./SuperUser.scss"></style>
<script src="./SuperUser"></script>
