<template>
  <a-modal
    :title="title"
    :width="1000"
    :visible="visible"
    :confirmLoading="confirmLoading"
    @ok="handleOk"
    @cancel="handleCancel">
    <a-spin :spinning="formLoading">
      <a-form-model ref="form" :model="form" :label-col="labelCol" :wrapper-col="wrapperCol" :rules="rules">
        <a-row :gutter="24">
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item label="菜单名称" prop="name" hasFeedback>
              <a-input placeholder="请输入菜单名称" v-model="form.name" />
            </a-form-model-item>
          </a-col>
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item label="所属应用" prop="menuType">
              <a-select style="width: 100%" v-model="form.menuType" placeholder="请选择应用分类">
                <a-select-option
                  v-for="(item, index) in appData"
                  :key="index"
                  :value="item.code"
                  @click="changeApplication(item.code)">{{ item.name }}</a-select-option>
              </a-select>
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row :gutter="gutter">
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item label="父级菜单" prop="parentId">
              <!-- v-decorator="['id', {rules: [{ required: true, message: '请选择父级菜单！' }]}]"  -->
              <a-tree-select
                style="width: 100%"
                v-model="form.parentId"
                :dropdownStyle="{ maxHeight: '300px', overflow: 'auto' }"
                :treeData="menuTreeData"
                :replaceFields="{
                  key: 'id',
                  title: 'name',
                  value: 'id'
                }"
                @change="menuTreeChange"
                placeholder="请选择父级菜单">
                <span slot="title" slot-scope="{ title }">{{ title }}
                </span>
              </a-tree-select>
            </a-form-model-item>
          </a-col>
          <div v-if="pidShow">
            <a-col :md="mdSize" :sm="smSize" >
              <a-form-model-item prop="component" hasFeedback>
                <span slot="label">
                  <a-tooltip
                    title="顶级(目录)菜单填写：RouteView(不带面包屑)，PageView(带面包屑),子级(菜单级)内链打开http链接填写：menu/menuList">
                    <a-icon type="question-circle-o" />
                  </a-tooltip>&nbsp;
                  前端组件
                </span>
                <a-input placeholder="请输入前端组件" v-model="form.component" :disabled="!pidShow" />
              </a-form-model-item>
            </a-col>
          </div>
          <div v-else>
            <a-col :md="mdSize" :sm="smSize">
              <a-form-model-item prop="redirect" hasFeedback>
                <span slot="label">
                  <a-tooltip title="如需打开首页加载此目录下菜单，请填写加载菜单路由，设为首页后其他设置的主页将被替代">
                    <a-icon type="question-circle-o" />
                  </a-tooltip>&nbsp;
                  重定向
                </span>
                <a-input prop="redirect" placeholder="请输入重定向地址" :disabled="pidShow" v-model="form.redirect" />
              </a-form-model-item>
            </a-col>
          </div>
        </a-row>
        <a-row :gutter="gutter">
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item prop="path" hasFeedback>
              <span slot="label">
                <a-tooltip title="浏览器显示的URL，例：/menu，对应打开页面为菜单页面">
                  <a-icon type="question-circle-o" />
                </a-tooltip>&nbsp;
                路由地址
              </span>
              <a-input placeholder="请输入路由" v-model="form.path" />
            </a-form-model-item>
          </a-col>
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item label="图标">
              <a-input placeholder="请选择图标" v-model="form.icon" :disabled="true" v-decorator="['icon']">
                <a-icon slot="addonAfter" @click="openIconSele()" type="setting" />
              </a-input>
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-divider />
        <a-row :gutter="gutter">
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item prop="title" hasFeedback>
              <span slot="label">
                <a-tooltip title="菜单显示多语言名字的绑定值">
                  <a-icon type="question-circle-o" />
                </a-tooltip>&nbsp;
                多语言值
              </span>
              <a-input style="width: 100%" v-model="form.title" />
            </a-form-model-item>
          </a-col>
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item label="排序" prop="order" hasFeedback>
              <a-input-number style="width: 100%" v-model="form.order" />
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row :gutter="gutter">
          <!-- <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item label="是否缓存">
              <a-switch checkedChildren="是" v-model="form.keepAlive" unCheckedChildren="否" />
            </a-form-model-item>
          </a-col> -->
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item label="是否可见">
              <a-switch checkedChildren="是" v-model="form.show" unCheckedChildren="否" />
            </a-form-model-item>
          </a-col>
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item>
              <span slot="label">
                <a-tooltip
                  title="为True,则不显示子菜单,只显示当前菜单">
                  <a-icon type="question-circle-o" />
                </a-tooltip>&nbsp;
                隐藏子类
              </span>
              <a-switch checkedChildren="是" v-model="form.hideChildren" unCheckedChildren="否" @change="menuTreeChange" />
            </a-form-model-item>
          </a-col>
        </a-row>
      </a-form-model>
      <!-- <a-row :gutter="gutter">
           <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item
              :labelCol="labelCol"
              :wrapperCol="wrapperCol"
              label="备注"
              hasFeedback
            >
              <a-input placeholder="请输入备注" v-decorator="['remark']"></a-input>
            </a-form-model-item>
          </a-col>
        </a-row> -->
    </a-spin>
    <a-modal
      :width="850"
      :visible="visibleIcon"
      @cancel="handleCancelIcon"
      footer=""
      :mask="false"
      :closable="false"
      :destroyOnClose="true">
      <icon-selector v-model="form.icon" @change="handleIconChange" />
    </a-modal>
  </a-modal>
</template>

<script>

import IconSelector from '@/components/IconSelector'
import { sysDict } from '@/api/system'
function createForm () {
  return {
    menuType: 'Business',
    icon: 'none',
    show: true
  }
}

export default {
  components: { IconSelector },
  data () {
    return {
      labelCol: {
        xs: { span: 24 },
        sm: { span: 6 }
      },
      wrapperCol: {
        xs: { span: 24 },
        sm: { span: 16 }
      },
      gutter: 24,
      mdSize: 12,
      smSize: 24,
      formLoading: false,
      pidShow: true,
      title: '添加菜单',
      visible: false,
      formType: 'add',
      visibleIcon: false,
      confirmLoading: false,
      menuType: 'Child',
      typeData: [],
      appData: [],
      menuTreeData: [],
      rules: {
        path: [{ required: true, min: 1, message: '请输入路由！' }],
        name: [{ required: true, min: 1, message: '请输入菜单名称！' }],
        menuType: [{ required: true, message: '请选择应用分类' }],
        parentId: [{ required: true, message: '请选择父级菜单！' }],
        component: [{ required: true, message: '请输入前端组件！' }],
        order: [{ required: true, message: '请输入排序值！' }],
        redirect: [{ required: true, message: '请输入重定向！' }],
        title: [{ required: true, message: '请输入多语言值！' }]
      },
      form: {}
    }
  },
  methods: {
    // 打开页面初始化
    detail (value) {
      // 获取下拉数据
      this.getDropdown()
      if (value) {
        this.form = JSON.parse(JSON.stringify(value))
        this.formType = 'edit'
        // 如果是父级菜单
        if (value.parentId === 0) {
          this.menuType = 'Parent'
          this.pidShow = false
        } else {
          this.menuType = 'Child'
          this.pidShow = true
        }
        this.title = '编辑菜单'
      } else {
        this.form = createForm()
        this.title = '新增菜单'
      }
      this.visible = true
      this.changeApplication(this.form.menuType)
    },
    // 获取下拉数据
    async getDropdown () {
      const res = await this.$http.get('/sysApps')
      if (res.code === 200) {
        this.appData = res.result
      }
      sysDict({ code: 'MenuLevel' }).then((res) => {
        if (res.code === 200) {
          this.typeData = res.result
        } else {
          this.$message.warning(res.message)
        }
      })
    },
    // 切换父子类菜单
    menuTreeChange (value) {
      if (value === 0 || value === true) {
        this.form.component = 'RouteView'
        this.pidShow = false
      } else {
        this.form.component = ''
        this.pidShow = true
      }
      this.$refs.form.clearValidate()
    },
    hideChildrenChange (value) {
      if (value === true) {
        this.form.component = ''
        this.pidShow = true
      } else {
        this.form.component = ''
        this.pidShow = true
      }
      this.$refs.form.clearValidate()
    },
    changeApplication (value) {
      this.$http.get('/sysMenu/tree', { params: { menuType: value } }).then(res => {
        if (res.code === 200) {
          const parentMenu = {
            id: 0,
            name: '顶级菜单',
            children: []
          }
          res.result.unshift(parentMenu)
          this.menuTreeData = res.result
        } else {
          this.$message.warning(res.message)
        }
      })
    },
    openIconSele () {
      this.visibleIcon = true
    },
    handleIconChange (icon) {
      this.form.icon = icon
      this.visibleIcon = false
    },
    handleCancelIcon () {
      this.visibleIcon = false
    },
    close () {
      this.$emit('close')
      this.visible = false
    },
    handleOk () {
      this.$refs.form.validate(valid => {
        if (valid) {
          this.confirmLoading = true
          if (this.formType === 'edit') {
            this.$http.put('/sysMenu', this.form).then(res => {
              if (res.code === 200) {
                this.$emit('ok')
                this.visible = false
                this.$message.success('保存成功')
              }
            }).finally(() => {
              this.confirmLoading = false
            })
          } else {
            this.$http.post('/sysMenu', this.form).then(res => {
              if (res.code === 200) {
                this.$emit('ok')
                this.visible = false
                this.$message.success('保存成功')
              }
            }).finally(() => {
              this.confirmLoading = false
            })
          }
        }
      })
    },
    handleCancel () {
      this.close()
    }
  }

}
</script>
