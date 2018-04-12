<template>
    <div v-if="apiData">
       <!-- <h1>{{ apiData.name }}  - versjon {{ apiData.version }}</h1> -->

        <div class="row">
            <div class="col-lg-8">

                <p>Leverandør: {{ apiData.Vendor }}</p>
                <p>Systemansvarlig: <a href="" class="badge badge-info"><i class="fas fa-user"></i> {{ apiData.SystemOwner }}</a></p>

                <h2>Lenker</h2>
                    <resource-links v-bind:application-id="applicationId"></resource-links>
                <hr />

                <h2>Kostnader</h2>
                <hr />
                <p>@Html.DisplayNameFor(model => model.AnnualFee): @Html.DisplayFor(model => model.AnnualFee)</p>
                <p>@Html.DisplayNameFor(model => model.InitialCost): @Html.DisplayFor(model => model.InitialCost)</p>

                <h2>Avtale</h2>
                <hr />
                <p>Detaljer om avtale.</p>

                <h2>Drift</h2>
                <hr />
                <p>Driftsleverandør: *ukjent*</p>
                <p>@Html.DisplayNameFor(model => model.HostingLocation): @Html.DisplayFor(model => model.HostingLocation)</p>
                <p>@Html.DisplayNameFor(model => model.NumberOfUsers): @Html.DisplayFor(model => model.NumberOfUsers)</p>

                <p>
                    Superbrukere:
                    <a href="" class="badge badge-info"><i class="fas fa-user"></i>Hans Hansen</a>
                    <a href="" class="badge badge-info"><i class="fas fa-user"></i>Jan Johansen</a>
                    <a href="" class="badge badge-info"><i class="fas fa-user"></i>Ole Olsen</a>
                </p>

                <h2>Vurderingskriterier</h2>
                <hr />
                <p>Brukertilfredshet:</p>
                <p>Applikasjonsmodenhet:</p>
                <p>Skalerbarhet:</p>

            </div>
            <div class="col-lg-4">
                <div class="card">
                    <div class="card-header bg-info text-white">@Html.DisplayNameFor(model => model.Sectors)</div>
                    <div class="card-body">
                        <application-sectors v-bind:apiData="apiData.sectors" v-bind:applicationId="applicationId"></application-sectors>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script src="./Application.js"></script>
<style src="./Application.scss"></style>
